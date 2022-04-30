using Skyblock.UI.ViewModels.List;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skyblock.Logic;
using System.Windows.Input;
using Skyblock.Common.Domain;

namespace Skyblock.UI.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private bool refreshing;
        private readonly AuctionFilterLogic _logic;

        public ApiLoaderViewModel ApiLoaderVM { get; set; }
        public MenuViewModel MenuVM { get; set; }
        public AuctionFilterViewModel AuctionFilterVM { get; set; }
        public BitPricesViewModel BitPricesVM { get; set; }
        public AccessoryPricesViewModel AccessoryPricesVM { get; set; }
        public SearchFilterViewModel SearchFilterVM { get; set; }
        public ICommand RefreshListCommand { get; set; }

        public MainWindowViewModel(AuctionFilterLogic logic)
        {
            _logic = logic;
            ApiLoaderVM = new ApiLoaderViewModel();
            ApiLoaderVM.DataLoaded += UpdateAuctions;
            SearchFilterVM = new SearchFilterViewModel();
            BitPricesVM = new BitPricesViewModel(_logic);
            AccessoryPricesVM = new AccessoryPricesViewModel(_logic);
            AuctionFilterVM = new AuctionFilterViewModel(_logic);
            MenuVM = new MenuViewModel(BitPricesVM, AccessoryPricesVM);
            RefreshListCommand = new AsyncDelegateCommand(RefreshList, CanRefreshList);
        }
        public async Task InitializeAsync()
        {
            await ApiLoaderVM.InitializeAsync();
        }

        private async Task RefreshList(object _)
        {
            refreshing = true;
            await AuctionFilterVM.RefreshList(SearchFilterVM.ToAuctionQuery());
            refreshing = false;
        }

        private bool CanRefreshList(object _)
        {
            return ApiLoaderVM.Auctions is not null && !refreshing;
        }

        private async void UpdateAuctions(IList<Auction> auctions)
        {
            BitPricesVM.Auctions = auctions;
            AccessoryPricesVM.Auctions = auctions;
            AuctionFilterVM.Auctions = auctions;
            await RefreshList(null);
        }


    }
}
