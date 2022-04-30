using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.Domain
{
    public class BitPrice
    {
        public string ItemName { get; set; }
        public int PriceInBits { get; set; }
        public int CoinsPerBit { get; set; }
        public int ItemCount { get; set; }
        public double AveragePrice { get; set; }
        public ObservableCollection<Auction> Auctions { get; set; }

        public BitPrice()
        {
            ItemName = "";
            Auctions = new();
        }

        public override string ToString()
        {
            return $"{CoinsPerBit,5:N0} ({ItemCount,3:N0}): {ItemName} ({AveragePrice:N0})";
        }
    }
}
