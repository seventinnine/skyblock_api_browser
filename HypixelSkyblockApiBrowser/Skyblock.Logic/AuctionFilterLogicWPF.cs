using Skyblock.Common.Domain;
using Skyblock.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Logic
{
    public class AuctionFilterLogicWPF
    {
        public async Task<IEnumerable<BitPrice>> CalculateBitPricesAsync(IList<Auction> auctions)
        {
            return await auctions.ApplyBitPriceFilterAsync();
        }

        public async Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(IList<Auction> auctions)
        {
            return await auctions.ApplyAccessoryPriceFilterAsync(new AccessoryQuery());
        }

        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions)
        {
            return await auctions.ApplyAuctionFilterNotPagedAsync(query);
        }

    }
}
