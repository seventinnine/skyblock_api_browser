using System.Collections.ObjectModel;

namespace Skyblock.Common.Domain
{
    public class AccessoryPrice
    {
        public string ItemName { get; set; }
        public Rarity Rarity { get; set; }
        public int ItemCount { get; set; }
        public double AveragePrice { get; set; }
        public ObservableCollection<Auction> Auctions { get; set; }

        public AccessoryPrice()
        {
            ItemName = "";
            Rarity = Rarity.Any;
            Auctions = new();
        }

        public override string ToString()
        {
            return $"({ItemCount,3:N0}): {ItemName} ({AveragePrice:N0})";
        }

    }
}