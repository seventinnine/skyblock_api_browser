using Skyblock.Client;
using Skyblock.Common.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skyblock.UI.ViewModels
{
    public delegate void DataLoadedEvent(IList<Auction> data);
    public class ApiLoaderViewModel : NotifyPropertyChanged
    {
        private const int SLEEP_TIME = 20_000;
        private readonly APIClient client = new();
        private int currentProgress;
        private bool newDataArrived;
        public IList<Auction> Auctions { get; set; }

        public int CurrentProgress
        {
            get => currentProgress;
            set => Set(ref currentProgress, value);
        }
        public ICommand ReloadDataCommand { get; set; }
        public event DataLoadedEvent DataLoaded;

        public ApiLoaderViewModel()
        {
            ReloadDataCommand = new AsyncDelegateCommand(ReloadData, CanReloadData);
            client.ProgressChanged += UpdateProgressBar;
        }
        public async Task InitializeAsync()
        {
            // initial load invokes DataLoaded
            await LoadData(true);
        }

        private void UpdateProgressBar(double percentage)
        {
            CurrentProgress = (int)(percentage * 100);
        }

        private Task ReloadData(object _)
        {
            newDataArrived = false;
            if (Auctions is not null) DataLoaded?.Invoke(Auctions);
            return Task.CompletedTask;
        }

        private bool CanReloadData(object _)
        {
            return newDataArrived;
        }

        private async Task LoadData(bool invoke)
        {
            while (true)
            {
                Auctions = await client.GetAHData();
                if (invoke) DataLoaded?.Invoke(Auctions);
                CurrentProgress = 100;
                newDataArrived = true;
                await Task.Delay(SLEEP_TIME);
                invoke = false;
            }
        }
    }
}
