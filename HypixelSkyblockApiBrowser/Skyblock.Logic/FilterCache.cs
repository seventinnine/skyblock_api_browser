using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public class FilterCache : IFilterCache
    {
        private readonly object auctionLocker = new();
        private readonly object bitsLocker = new();
        private readonly IDictionary<AuctionQuery, IList<AuctionDTO>> cachedAuctions = new Dictionary<AuctionQuery, IList<AuctionDTO>>();
        private IList<BitPrice> cachedBits = new List<BitPrice>();

        public void Reset()
        {
            lock (auctionLocker)
            {
                cachedAuctions.Clear();
            }
        }

        public bool TryGetFromCache(AuctionQuery query, [NotNullWhen(true)] out IList<AuctionDTO>? cachedResult)
        {
            lock (auctionLocker)
            {
                var found = cachedAuctions.ContainsKey(query);
                cachedResult = found ? cachedAuctions[query] : null;
                return found;
            }
        }

        public bool TryGetFromCache([NotNullWhen(true)] out IList<BitPrice>? cachedResult)
        {
            lock (auctionLocker)
            {
                var found = cachedBits is not null;
                cachedResult = found ? cachedBits : null;
                return found;
            }
    }

        public void Put(AuctionQuery query, IList<AuctionDTO> filteredResult)
        {
            lock (auctionLocker)
            {
                if (!cachedAuctions.ContainsKey(query))
                    cachedAuctions.Add(query, new List<AuctionDTO>(filteredResult));
            }
        }

        public void Put(IList<BitPrice> filteredResult)
        {
            lock (bitsLocker)
            {
                cachedBits = new List<BitPrice>(filteredResult);
            }
        }
    }
}