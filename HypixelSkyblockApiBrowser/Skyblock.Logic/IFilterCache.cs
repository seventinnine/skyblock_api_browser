using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public interface IFilterCache
    {
        void Reset();
        bool TryGetFromCache(AuctionQuery query, [NotNullWhen(true)] out IList<AuctionDTO>? cachedResult);
        bool TryGetFromCache([NotNullWhen(true)] out IList<BitPrice>? cachedResult);
        void Put(AuctionQuery query, IList<AuctionDTO> filteredResult);
        void Put(IList<BitPrice> filteredResult);
    }
}