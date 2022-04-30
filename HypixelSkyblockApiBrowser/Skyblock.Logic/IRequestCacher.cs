using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Skyblock.Common.Domain;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public interface IRequestCacher
    {
        void ClearCache();
        bool TryGetFromCache(AuctionQuery query, [NotNullWhen(true)] out IEnumerable<Auction> cachedResult);
        bool TryGetFromCache([NotNullWhen(true)] out IEnumerable<BitPrice> cachedResult);
        void Cache(AuctionQuery query, IEnumerable<Auction> filteredResult);
        void Cache(IEnumerable<BitPrice> filteredResult);
    }
}