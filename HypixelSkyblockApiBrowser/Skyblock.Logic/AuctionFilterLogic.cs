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
using Skyblock.Common.DTOs;
using Skyblock.Client;

namespace Skyblock.Logic
{
    public class AuctionFilterLogic : IAuctionFilterLogic
    {
        public const int ItemCountBits = 5;
        public const int ItemCountAccessories = 1;
        
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
            var bitPrices = await auctionsCopy.ApplyFilterAsync();
            return mapper!.Map<IList<BitPriceDTO>>(bitPrices);
        }

        public async Task<IEnumerable<AccessoryPriceDTO>> CalculateAccessoryPricesAsync(AccessoryQuery query)
        {
            var auctionsCopy = apiClient.CurrentData.Get();
            var accessoryPrices = await auctionsCopy.ApplyFilterAsync(query);
            return mapper!.Map<IList<AccessoryPriceDTO>>(accessoryPrices);
        }

        public async Task<IEnumerable<AuctionDTO>> FilterAuctionsAsync(AuctionQuery query)
        {
            var auctionsCopy = apiClient.CurrentData.Get();
            var filteredAuctions = await auctionsCopy.ApplyFilterAsync(query);
            return mapper!.Map<IList<AuctionDTO>>(filteredAuctions);
        }

        #region Old

        public async Task<IEnumerable<BitPrice>> CalculateBitPricesAsync(IList<Auction> auctions)
        {
            return await auctions.ApplyFilterAsync();
        }

        public async Task<IEnumerable<AccessoryPrice>> CalculateAccessoryPricesAsync(IList<Auction> auctions)
        {
            return await auctions.ApplyFilterAsync(new AccessoryQuery());
        }

        public async Task<IEnumerable<Auction>> FilterAuctionsAsync(AuctionQuery query, IList<Auction> auctions)
        {
            return await auctions.ApplyFilterAsync(query);
        }

        #endregion

        #region private

        #endregion


    }

    static class AuctionFilterLogicExtensions
    {
        public static async Task<IList<BitPrice>> ApplyFilterAsync(this IList<Auction> auctions)
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

        public static async Task<IList<AccessoryPrice>> ApplyFilterAsync(this IList<Auction> auctions, AccessoryQuery query)
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

                    if (filtered.Count < AuctionFilterLogic.ItemCountAccessories) continue;

                    var firstXItems = filtered.Take(AuctionFilterLogic.ItemCountAccessories).ToList();
                    double avg = firstXItems.Average(it => it.StartingBid);
                    AccessoryPrice newEntry = new()
                    {
                        ItemName = item.ToString(),
                        ItemCount = filtered.Count,
                        Rarity = item.Rarity,
                        AveragePrice = (int)avg,
                        Auctions = new ObservableCollection<Auction>(firstXItems)
                    };
                    res.Add(newEntry);
                }
            });
            return res.OrderBy(ap => ap.AveragePrice).ToList();
        }

        public static async Task<IList<Auction>> ApplyFilterAsync(this IList<Auction> auctions, AuctionQuery query)
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

            var res = new List<Auction>();
            await Task.Run(() =>
            {
                res = auctions
                .Where(a => query.SelectedCategory != Category.Any || a.Category == query.SelectedCategory)
                .Where(a => query.SelectedRarity != Rarity.Any || a.Tier == query.SelectedRarity)
                .Where(a => query.MaxPrice > 0 || a.StartingBid <= query.MaxPrice)
                .Where(a => a.Bin == query.Bin)
                .Where(a => rx.IsMatch(a.ItemName))
                .Where(a => query.LoreContains.All(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)) || query.LoreContains.Count == 0)
                .Where(a => !query.LoreDoesNotContain.Any(item => a.ItemLore.Contains(item, StringComparison.InvariantCultureIgnoreCase)))
                .Where(a => query.MinimumStars == Constants.NoStars || a.ItemName.Contains(query.MinimumStars))
                .OrderBy(a => a.StartingBid)
                .ToList();
            });
            return res;
        }
    }
    
}
