using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Client
{
    public class CallResult
    {
        public bool Success { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalAuctions { get; set; }
        public long LastUpdated { get; set; }
        public List<AuctionData> Auctions { get; set; }
    }
}
