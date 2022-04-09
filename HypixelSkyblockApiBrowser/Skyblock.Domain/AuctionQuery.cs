using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skyblock.Domain
{
    public class AuctionQuery
    {
        private const double DoubleComparisonTolerance = 0.1;

        [JsonProperty(Required = Required.DisallowNull)]
        public string ItemName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public bool Bin { get; set; }
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity SelectedRarity { get; set; }
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Category SelectedCategory { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public double MaxPrice { get; set; }
        [JsonProperty(Required = Required.DisallowNull)]
        public List<string> LoreContains { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public List<string> LoreDoesNotContain { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null or not AuctionQuery) return false;
            var other = (AuctionQuery) obj;

            return ItemName.Equals(other.ItemName)
                   && Bin == other.Bin
                   && SelectedRarity == other.SelectedRarity
                   && SelectedCategory == other.SelectedCategory
                   && Math.Abs(MaxPrice - other.MaxPrice) < DoubleComparisonTolerance
                   && LoreContains.SequenceEqual(other.LoreContains)
                   && LoreDoesNotContain.SequenceEqual(other.LoreDoesNotContain);
        }
    }
}