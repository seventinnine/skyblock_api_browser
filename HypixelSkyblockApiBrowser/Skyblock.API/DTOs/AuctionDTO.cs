using Newtonsoft.Json;
using Skyblock.Common.Domain;

namespace Skyblock.API.DTOs
{
    public class AuctionDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string UUID { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ItemName { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int PetLevel { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ItemLore { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int StartingBid { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public bool Bin { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Auctioneer { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Category Category { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Rarity Tier { get; set; }
    }
}
