using Skyblock.Domain;
using Skyblock.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Skyblock.Logic.Helper;

namespace Skyblock.Logic
{
    public class AuctionFilterLogic : IAuctionFilterLogic
    {
        private const int ItemCount = 5;
        
        private readonly object auctionLocker = new();
        private readonly object bitsLocker = new();
        private IList<Auction> _auctions = new List<Auction>();
        private readonly RequestCacher cacher = new();
        
        public async Task<IEnumerable<BitPrice>> CalculateBitPricesAsync()
        {
            if (cacher.TryGetFromCache(out var cachedResult)) return cachedResult;
            var res = (await CalculateBitPricesAsync(_auctions)).ToList();
            cacher.Cache(res);
            return res;
        }
        
        public async Task<IEnumerable<BitPrice>> CalculateBitPricesAsync(IList<Auction> allAuctions)
        {
            var res = new List<BitPrice>();
            await Task.Run(() =>
            {
                Parallel.ForEach(BitPrices.Items, (item) =>
                {
                    var filtered = (from curr in allAuctions
                        where curr.Bin
                              && curr.ItemName.Contains(item.ItemName, StringComparison.InvariantCultureIgnoreCase)
                              && curr.ItemLore.Contains(item.ItemLore, StringComparison.InvariantCultureIgnoreCase)
                        orderby curr.StartingBid ascending
                        select curr).ToList();

                    var itemCount = filtered.Count;
                    if (itemCount < ItemCount) return;

                    var firstXItems = filtered.Take(ItemCount).ToList();
                    var avg = firstXItems.Average(it => it.StartingBid);
                    var coinsPerBit = avg / item.BitPrice;
                    var newEntry = new BitPrice
                    {
                        ItemName = item.ToString(),
                        PriceInBits = item.BitPrice,
                        CoinsPerBit = (int) coinsPerBit,
                        ItemCount = itemCount,
                        AveragePrice = avg,
                        Auctions = new ObservableCollection<Auction>(firstXItems)
                    };

                    lock (res)
                    {
                        res.Add(newEntry);
                    }
                });
            });
            
            return res.OrderByDescending(item => item.CoinsPerBit);
        }
        
        public async Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPrices(IList<Auction> allAuctions)
        {
            var res = new List<AccessoryPrice>();
            await Task.Run(() =>
            {
                Parallel.ForEach(Accessories.Items, (item) =>
                {
                    var filtered = (from curr in allAuctions
                        where curr.Bin
                            && curr.ItemName.Contains(item.ItemName, StringComparison.InvariantCultureIgnoreCase)
                            && (item.Rarity == Rarity.Any || curr.Tier == item.Rarity)
                        orderby curr.StartingBid ascending
                        select curr).ToList();

                    var itemCount = filtered.Count;
                    Debug.WriteLine($"{item.ItemName} ({itemCount})");
                    if (itemCount < 1) return;

                    var firstXItems = filtered.Take(ItemCount).ToList();
                    var avg = firstXItems.Average(it => it.StartingBid);
                    var newEntry = new AccessoryPrice
                    {
                        ItemName = item.ToString(),
                        ItemCount = itemCount,
                        Rarity = item.Rarity,
                        AveragePrice = (int)avg,
                        Auctions = new ObservableCollection<Auction>(filtered)
                    };

                    lock (res)
                    {
                        res.Add(newEntry);
                    }
                });
            });
            
            return res.OrderBy(item => item.AveragePrice);
        }

        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(IList<Auction> allAuctions, SearchFilter filter)
        {
            IEnumerable<Auction> res = new List<Auction>();
            await Task.Run(() =>
            {
                res = from curr in allAuctions.AsParallel()
                      where curr.Bin
                      && curr.ItemName.Contains(filter.ItemName, StringComparison.InvariantCultureIgnoreCase)
                      && curr.ItemLore.Contains(filter.ItemLore, StringComparison.InvariantCultureIgnoreCase)
                      select curr;

                if (filter.MaxPrice > 0)
                    res = res.Where(a => a.StartingBid <= filter.MaxPrice);
                if (filter.SelectedRarity != Rarity.Any)
                    res = res.Where(a => a.Tier == filter.SelectedRarity);
                if (filter.SelectedCategory != Category.Any)
                    res = res.Where(a => a.Category == filter.SelectedCategory);

            });
            
            return res;
        }
        
        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query)
        {
            IList<Auction> filterAuctionsAsync = null;

            // try read result from cache
            if (cacher.TryGetFromCache(query, out var cachedResult)) return cachedResult;

            await Task.Run(() => filterAuctionsAsync = _auctions.ApplyFilter(query));
            
            cacher.Cache(query, filterAuctionsAsync);
            
            return filterAuctionsAsync;
        }
        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions)
        {
            this._auctions = auctions;
            return await FilterAuctionsAsync(query);
        }
    }

    public static class AuctionFilterLogicExtensions
    {
        public static IList<Auction> ApplyFilter(this IList<Auction> auctions, AuctionQuery query)
        {
            IEnumerable<Auction> res = auctions;
            if (query.SelectedCategory != Category.Any)
                res = res.Where(a => a.Category == query.SelectedCategory);
            if (query.SelectedRarity != Rarity.Any)
                res = res.Where(a => a.Tier == query.SelectedRarity);
            if (query.MaxPrice > 0)
                res = res.Where(a => a.StartingBid <= query.MaxPrice);
            res =
                from curr in res
                where curr.Bin == query.Bin
                      && curr.ItemName.Contains(query.ItemName, StringComparison.InvariantCultureIgnoreCase)
                      && (query.LoreContains.All(item => curr.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)) || query.LoreContains.Count == 0)
                      && !query.LoreDoesNotContain.Any(item => curr.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase))
                      select curr;
            return res.OrderBy(a => a.StartingBid).ToList();

        }
    }
    
}
