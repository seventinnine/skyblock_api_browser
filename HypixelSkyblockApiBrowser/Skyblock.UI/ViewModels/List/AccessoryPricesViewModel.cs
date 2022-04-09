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
    public class AccessoryPricesViewModel : NotifyPropertyChanged
    {
        public IList<Auction> Auctions { get; set; }
        private readonly AuctionFilterLogic logic;
        private ObservableCollection<AccessoryPrice> accessoryPrices;

        public ObservableCollection<AccessoryPrice> AccessoryPrices { get => accessoryPrices; set => Set(ref accessoryPrices, value); }

        public AccessoryPricesViewModel(AuctionFilterLogic logic)
        {
            this.logic = logic;
        }

        public async Task InitializeAsync()
        {
            if (Auctions is not null)
            {
                var res = await logic.CalculateAccessoryPrices(Auctions);
                AccessoryPrices = new ObservableCollection<AccessoryPrice>(res);
            }
        }
    }
}
