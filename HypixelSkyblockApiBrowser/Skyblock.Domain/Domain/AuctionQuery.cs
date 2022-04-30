using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skyblock.Common.Domain
{
    public class AuctionQuery
    {
        [JsonProperty(Required = Required.Always)]
        public string ItemName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool Bin { get; set; }

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity SelectedRarity { get; set; }

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Category SelectedCategory { get; set; }


        [JsonProperty(Required = Required.Always)]
        public double MaxPrice { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<string> LoreContains { get; set; }


        [JsonProperty(Required = Required.Always)]
        public List<string> LoreDoesNotContain { get; set; }


        [JsonProperty(Required = Required.Always)]
        public string MinimumStars { get; set; }

        public AuctionQuery(string itemName, bool bin, Rarity selectedRarity, Category selectedCategory, double maxPrice, List<string> loreContains, List<string> loreDoesNotContain, string minimumStars)
        {
            ItemName = itemName;
            Bin = bin;
            SelectedRarity = selectedRarity;
            SelectedCategory = selectedCategory;
            MaxPrice = maxPrice;
            LoreContains = loreContains;
            LoreDoesNotContain = loreDoesNotContain;
            MinimumStars = minimumStars;
        }



        public override bool Equals(object? obj)
        {
            if (obj is null or not AuctionQuery) return false;
            var other = (AuctionQuery)obj;

            return ItemName.Equals(other.ItemName)
                   && Bin == other.Bin
                   && SelectedRarity == other.SelectedRarity
                   && SelectedCategory == other.SelectedCategory
                   && Math.Abs(MaxPrice - other.MaxPrice) < Constants.DoubleComparisonToleranceLow
                   && LoreContains.SequenceEqual(other.LoreContains)
                   && LoreDoesNotContain.SequenceEqual(other.LoreDoesNotContain)
                   && MinimumStars.Equals(other.MinimumStars);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemName, Bin, SelectedRarity, SelectedCategory, MaxPrice, LoreContains, LoreDoesNotContain, MinimumStars);
        }
    }
}