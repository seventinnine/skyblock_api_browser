using System.Collections.Generic;
using System.Threading.Tasks;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public interface IAuctionFilterLogic
    {
        Task<IEnumerable<BitPriceDTO>> CalculateBitPricesAsync();
        Task<IEnumerable<AccessoryPriceDTO>> CalculateAccessoryPricesAsync(AccessoryQuery query);
        Task<PagedResult<AuctionDTO>> FilterAuctionsAsync(AuctionQuery query);
    }
}