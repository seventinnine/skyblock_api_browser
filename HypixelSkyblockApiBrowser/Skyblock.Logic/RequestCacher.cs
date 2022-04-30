using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Skyblock.Common.Domain;
using Skyblock.Logic.Models;

namespace Skyblock.Logic
{
    public class RequestCacher : IRequestCacher
    {
        private readonly object auctionLocker = new();
        private readonly object bitsLocker = new();
        private readonly IDictionary<AuctionQuery, IEnumerable<Auction>> cachedAuctions = new Dictionary<AuctionQuery, IEnumerable<Auction>>();
        private IEnumerable<BitPrice> cachedBits;

        public void ClearCache()
        {
            lock (auctionLocker)
            {
                cachedAuctions.Clear();
            }
        }

        public bool TryGetFromCache(AuctionQuery query, [MaybeNullWhen(false)] out IEnumerable<Auction> cachedResult)
        {
            lock (auctionLocker)
            {
                var found = cachedAuctions.ContainsKey(query);
                cachedResult = found ? cachedAuctions[query] : null;
                return found;
            }
        }

        public bool TryGetFromCache(out IEnumerable<BitPrice> cachedResult)
        {
            lock (bitsLocker)
            {
                var found = cachedBits is not null;
                cachedResult = found ? cachedBits : null;
                return found;
            }
        }

        public void Cache(AuctionQuery query, IEnumerable<Auction> filteredResult)
        {
            lock (auctionLocker)
            {
                cachedAuctions.Add(query, filteredResult);
            }
        }

        public void Cache(IEnumerable<BitPrice> filteredResult)
        {
            lock (bitsLocker)
            {
                cachedBits = filteredResult;
            }
        }
    }
}