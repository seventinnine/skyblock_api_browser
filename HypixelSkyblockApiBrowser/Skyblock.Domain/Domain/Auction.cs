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

        public Auction()
        {
            ItemName = "";
            ItemLore = "";
            Auctioneer = "";
            UUID = "";
        }

        public int CompareTo(object? obj)
        {
            if (obj is null or not Auction) throw new ArgumentException("Object is not an Auction");

            var other = (Auction)obj;
            return StartingBid.CompareTo(other.StartingBid);
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
