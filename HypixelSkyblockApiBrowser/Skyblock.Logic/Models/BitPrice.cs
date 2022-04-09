using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyblock.Domain;

namespace Skyblock.Logic.Models
{
    public class BitPrice : NotifyPropertyChanged
    {
        private string itemName;
        private int bitPrice;
        private int coinsPerBit;
        private int itemCount;
        private double averagePrice;

        public string ItemName { get => itemName; set => Set(ref itemName, value); }
        public int PriceInBits { get => bitPrice; set => Set(ref bitPrice, value); }
        public int CoinsPerBit { get => coinsPerBit; set => Set(ref coinsPerBit, value); }
        public int ItemCount { get => itemCount; set => Set(ref itemCount, value); }
        public double AveragePrice { get => averagePrice; set => Set(ref averagePrice, value); }
        public ObservableCollection<Auction> Auctions { get; set; }

        public override string ToString()
        {
            return $"{CoinsPerBit,5:N0} ({ItemCount,3:N0}): {ItemName} ({AveragePrice:N0})";
        }
    }
}
