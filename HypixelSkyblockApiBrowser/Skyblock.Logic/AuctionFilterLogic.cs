using Skyblock.Common;
using Skyblock.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Skyblock.Logic.Helper;
using System.Text.RegularExpressions;
using AutoMapper;
using Skyblock.Common.Domain;
using System.Data.Entity;
using Skyblock.Common.DTOs;

namespace Skyblock.Logic
{
    public class AuctionFilterLogic : IAuctionFilterLogic
    {
        private const int ItemCount = 5;
        
        private IList<Auction> _auctions = new List<Auction>();
        private readonly FilterCache cacher = new();
        private readonly IMapper? mapper;

        public AuctionFilterLogic(IMapper? mapper = null)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BitPrice>> CalculateBitPricesAsync()
        {
            if (cacher.TryGetFromCache(out var cachedResult)) return cachedResult;
            var res = (await CalculateBitPricesAsync(_auctions)).ToList();
            cacher.Put(res);
            return res;
        }

        public async Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(AccessoryQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuctionDTO>> FilterAuctionsAsync(AuctionQuery query)
        {
            // try read result from cache
            if (cacher.TryGetFromCache(query, out var cachedResult)) return mapper!.Map<IList<AuctionDTO>>(cachedResult);

            // queue for evaluation

            IList<Auction> filteredAuctions = await _auctions.AsQueryable().ApplyFilterAsync(query);
            var mappedAuctions = mapper!.Map<IList<AuctionDTO>>(filteredAuctions);
            cacher.Put(query, mappedAuctions);
            return mappedAuctions;
        }

        #region Old

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
                        CoinsPerBit = (int)coinsPerBit,
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

        public async Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(IList<Auction> allAuctions)
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

        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions)
        {
            this._auctions = auctions;
            return await auctions.AsQueryable().ApplyFilterAsync(query);
        }

        #endregion

    }

    public static class AuctionFilterLogicExtensions
    {
        public static async Task<IList<Auction>> ApplyFilterAsync(this IQueryable<Auction> auctions, AuctionQuery query)
        {
            Regex? rx = null;
            try
            {
                rx = new Regex(query.ItemName, options: RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Could not parse regex'{query.ItemName}' AuctionFilterLogic.ApplyFilterAsync", ex);
                return new List<Auction>();
            }

            return await auctions
                .Where(a => query.SelectedCategory != Category.Any || a.Category == query.SelectedCategory)
                .Where(a => query.SelectedRarity != Rarity.Any || a.Tier == query.SelectedRarity)
                .Where(a => query.MaxPrice > 0 || a.StartingBid <= query.MaxPrice)
                .Where(a => a.Bin == query.Bin)
                .Where(a => rx.IsMatch(a.ItemName))
                .Where(a => query.LoreContains.All(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)) || query.LoreContains.Count == 0)
                .Where(a => !query.LoreDoesNotContain.Any(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)))
                .Where(a => query.MinimumStars == Constants.NoStars || a.ItemName.Contains(query.MinimumStars))
                .OrderBy(a => a.StartingBid)
                .ToListAsync();

        }
    }
    
}
