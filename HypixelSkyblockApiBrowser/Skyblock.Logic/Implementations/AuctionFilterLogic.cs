using Skyblock.Common;
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
using Skyblock.Common.DTOs;
using Skyblock.Client;
using Skyblock.Common.Interfaces;
using Skyblock.Logic.Implementations;
using Skyblock.Logic.Interfaces;

namespace Skyblock.Logic.Implementations
{
    public class AuctionFilterLogic : IAuctionFilterLogic
    {
        public const int ItemCountBits = 5;
        public const int ItemCountAccessories = 5;

        private readonly IMapper mapper;
        private readonly APIClient apiClient;

        public AuctionFilterLogic(IMapper mapper, APIClient apiClient)
        {
            this.mapper = mapper;
            this.apiClient = apiClient;
        }

        public async Task<IEnumerable<BitPriceDTO>> CalculateBitPricesAsync()
        {
            var auctionsCopy = apiClient.CurrentData.Get();
            var bitPrices = await auctionsCopy.ApplyBitPriceFilterAsync();
            return mapper.Map<IList<BitPriceDTO>>(bitPrices);
        }

        public async Task<IEnumerable<AccessoryPriceDTO>> CalculateAccessoryPricesAsync(AccessoryQuery query)
        {
            var auctionsCopy = apiClient.CurrentData.Get();
            var accessoryPrices = await auctionsCopy.ApplyAccessoryPriceFilterAsync(query);
            return mapper.Map<IList<AccessoryPriceDTO>>(accessoryPrices);
        }

        public async Task<PagedResult<AuctionDTO>> FilterAuctionsAsync(AuctionQuery query)
        {
            var auctionsCopy = apiClient.CurrentData.Get();
            var pagedAuctions = await auctionsCopy.ApplyAuctionFilterAsync(mapper, query);
            return pagedAuctions.MapTo<AuctionDTO>(mapper);
        }

    }
}

namespace Skyblock.Logic
{
    static class AuctionFilterLogicExtensions
    {
        public static async Task<IList<BitPrice>> ApplyBitPriceFilterAsync(this IList<Auction> auctions)
        {
            var res = new List<BitPrice>();

            await Task.Run(() =>
            {
                foreach (var item in BitPrices.Items)
                {
                    var filtered = auctions
                        .Where(a => a.Bin == true)
                        .Where(a => a.ItemName.Contains(item.ItemName, StringComparison.InvariantCultureIgnoreCase))
                        .Where(a => a.ItemLore.Contains(item.ItemLore, StringComparison.InvariantCultureIgnoreCase))
                        .OrderBy(a => a.StartingBid)
                        .ToList();

                    if (filtered.Count < AuctionFilterLogic.ItemCountBits) continue;

                    var firstXItems = filtered.Take(AuctionFilterLogic.ItemCountBits).ToList();
                    double avg = firstXItems.Average(it => it.StartingBid);
                    double coinsPerBit = avg / item.BitPrice;
                    BitPrice newEntry = new()
                    {
                        ItemName = item.ToString(),
                        PriceInBits = item.BitPrice,
                        CoinsPerBit = (int)coinsPerBit,
                        ItemCount = filtered.Count,
                        AveragePrice = avg,
                        Auctions = new ObservableCollection<Auction>(firstXItems)
                    };
                    res.Add(newEntry);
                }
            });

            return res.OrderByDescending(bp => bp.CoinsPerBit).ToList();
        }

        public static async Task<IList<AccessoryPrice>> ApplyAccessoryPriceFilterAsync(this IList<Auction> auctions, AccessoryQuery query)
        {
            var res = new List<AccessoryPrice>();

            await Task.Run(() =>
            {
                foreach (var item in Accessories.Items)
                {
                    var filtered = auctions
                        .Where(a => a.Bin == true)
                        .Where(a => a.ItemName.Contains(item.ItemName, StringComparison.InvariantCultureIgnoreCase))
                        .Where(a => item.Rarity == Rarity.Any || a.Tier == item.Rarity)
                        .OrderBy(a => a.StartingBid)
                        .ToList();

                    if (filtered.Count < 1) continue;

                    var firstXItems = filtered.Take(AuctionFilterLogic.ItemCountAccessories).ToList();
                    double avg = firstXItems.Average(it => it.StartingBid);
                    AccessoryPrice newEntry = new()
                    {
                        ItemName = item.ToString(),
                        ItemCount = filtered.Count,
                        Rarity = item.Rarity,
                        AveragePrice = filtered.ElementAt(0).StartingBid,
                        Auctions = new ObservableCollection<Auction>(firstXItems)
                    };
                    res.Add(newEntry);
                }
            });
            return res.OrderBy(ap => ap.AveragePrice).ToList();
        }

        public static async Task<PagedResult<Auction>> ApplyAuctionFilterAsync(this IList<Auction> auctions, IMapper mapper, AuctionQuery query)
        {
            Regex? rx = null;
            try
            {
                rx = new Regex(query.ItemName, options: RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Could not parse regex'{query.ItemName}' AuctionFilterLogic.ApplyFilterAsync", ex);
                return new PagedResult<Auction>();
            }

            PagedResult<Auction>? res = null;
            await Task.Run(() =>
            {
                res = auctions
                .Where(a => query.SelectedCategory == Category.Any || a.Category == query.SelectedCategory)
                .Where(a => query.SelectedRarity == Rarity.Any || a.Tier == query.SelectedRarity)
                .Where(a => query.MaxPrice <= Constants.DoubleComparisonToleranceLow || a.StartingBid <= query.MaxPrice)
                .Where(a => a.Bin == query.Bin)
                .Where(a => rx.IsMatch(a.ItemName))
                .Where(a => query.LoreContains.All(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)) || query.LoreContains.Count == 0)
                .Where(a => !query.LoreDoesNotContain.Any(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)))
                .Where(a => query.MinimumStars == Constants.NoStars || a.ItemName.Contains(query.MinimumStars))
                .OrderBy(a => a.StartingBid)
                .MakePage(query);
            });
            return res!;
        }

        public static async Task<IEnumerable<Auction>> ApplyAuctionFilterNotPagedAsync(this IList<Auction> auctions, AuctionQuery query)
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

            IList<Auction>? res = null;
            await Task.Run(() =>
            {
                res = auctions
                .Where(a => query.SelectedCategory == Category.Any || a.Category == query.SelectedCategory)
                .Where(a => query.SelectedRarity == Rarity.Any || a.Tier == query.SelectedRarity)
                .Where(a => query.MaxPrice <= Constants.DoubleComparisonToleranceLow || a.StartingBid <= query.MaxPrice)
                .Where(a => a.Bin == query.Bin)
                .Where(a => rx.IsMatch(a.ItemName))
                .Where(a => query.LoreContains.All(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)) || query.LoreContains.Count == 0)
                .Where(a => !query.LoreDoesNotContain.Any(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)))
                .Where(a => query.MinimumStars == Constants.NoStars || a.ItemName.Contains(query.MinimumStars))
                .OrderBy(a => a.StartingBid)
                .ToList();
            });
            return res!;
        }

        public static PagedResult<T> MakePage<T>(this IEnumerable<T> items, IPageable page)
        {
            if (page.AuctionsPerPage > 100) page.AuctionsPerPage = 100;
            int skip = (page.Page - 1) * page.AuctionsPerPage;
            var pagedItems = items.Skip(skip).Take(page.AuctionsPerPage).ToList();
            int totalItems = items.Count();
            int totalPages = totalItems / page.AuctionsPerPage + 1;
            return new PagedResult<T>(page.Page, totalPages, page.AuctionsPerPage, totalItems, pagedItems);
        }
    }

}
