using System.Collections.ObjectModel;
using Skyblock.Domain;

namespace Skyblock.Logic.Models
{
    public class AccessoryPrice : NotifyPropertyChanged
    {
        private string itemName;
        private Rarity rarity;
        private int itemCount;
        private double averagePrice;

        public string ItemName { get => itemName; set => Set(ref itemName, value); }
        public Rarity Rarity { get => rarity; set => Set(ref rarity, value); }
        public int ItemCount { get => itemCount; set => Set(ref itemCount, value); }
        public double AveragePrice { get => averagePrice; set => Set(ref averagePrice, value); }
        public ObservableCollection<Auction> Auctions { get; set; }

        public AccessoryPrice()
        {
            Rarity = Rarity.Any;
        }
    
        public override string ToString()
        {
            return $"({ItemCount,3:N0}): {ItemName} ({AveragePrice:N0})";
        }
        
    }
}