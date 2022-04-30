using System.Collections.Generic;
using System.Threading.Tasks;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public interface IAuctionFilterLogic
    {
        Task<IEnumerable<BitPrice>> CalculateBitPricesAsync();
        Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(AccessoryQuery query);
        Task<IEnumerable<AuctionDTO>> FilterAuctionsAsync(AuctionQuery query);

        Task<IEnumerable<BitPrice>> CalculateBitPricesAsync(IList<Auction> auctions);
        Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(IList<Auction> auctions);
        Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions);
    }
}