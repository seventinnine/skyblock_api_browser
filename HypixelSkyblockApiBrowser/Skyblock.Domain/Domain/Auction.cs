using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.Domain
{
    public class Auction : IComparable
    {
        public string ItemName { get; set; }
        public int PetLevel { get; set; }
        public string ItemLore { get; set; }
        public int StartingBid { get; set; }
        public bool Bin { get; set; }
        public string Auctioneer { get; set; }
        public string UUID { get; set; }
        public Category Category { get; set; }
        public Rarity Tier { get; set; }
        public int CompareTo(object obj)
        {
            if (obj is Auction otherAuction)
                return StartingBid.CompareTo(otherAuction.StartingBid);
            else
                throw new ArgumentException("Object is not an Auction");
        }
        public override string ToString()
        {
            return $"{Tier} {ItemName}: {StartingBid:N0} /viewauction {UUID}";
        }
        public string ViewAuction()
        {
            return $"/viewauction {UUID}";
        }

    }
}
