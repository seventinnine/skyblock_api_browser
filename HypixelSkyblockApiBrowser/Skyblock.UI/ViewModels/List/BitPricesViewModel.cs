using Skyblock.Common.Domain;
using Skyblock.Logic.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.UI.ViewModels.List
{
    public class BitPricesViewModel : NotifyPropertyChanged
    {
        public IList<Auction>? Auctions { get; set; }
        private readonly AuctionFilterLogicWPF logic;
        private ObservableCollection<BitPrice>? bitPrices;

        public ObservableCollection<BitPrice>? BitPrices { get => bitPrices; set => Set(ref bitPrices, value); }

        public BitPricesViewModel(AuctionFilterLogicWPF logic)
        {
            this.logic = logic;
        }

        public async Task InitializeAsync()
        {
            if (Auctions is not null)
            {
                var res = await logic.CalculateBitPricesAsync(Auctions);
                BitPrices = new ObservableCollection<BitPrice>(res);
            }
        }
    }
}
