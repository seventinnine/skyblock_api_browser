using System.Collections.Generic;
using System.Threading.Tasks;
using Skyblock.Common.Domain;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public interface IAuctionFilterLogic
    {
        Task<IEnumerable<BitPrice>> CalculateBitPricesAsync();
        Task<IEnumerable<BitPrice>> CalculateBitPricesAsync(IList<Auction> auctions);
        Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPrices(IList<Auction> auctions);
        Task<IEnumerable<Auction>> FilterAuctionsAsync(IList<Auction> auctions, SearchFilter filter);
        Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions);
        Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query);
    }
}