using Skyblock.Domain;
using Skyblock.Logic;
using Skyblock.Logic.Models;
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
        public IList<Auction> Auctions { get; set; }
        private readonly AuctionFilterLogic logic;
        private ObservableCollection<BitPrice> bitPrices;

        public ObservableCollection<BitPrice> BitPrices { get => bitPrices; set => Set(ref bitPrices, value); }

        public BitPricesViewModel(AuctionFilterLogic logic)
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
