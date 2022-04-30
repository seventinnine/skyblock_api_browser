using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Skyblock.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.DTOs
{
    public class AccessoryPriceDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string ItemName { get; set; }

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity Rarity { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int ItemCount { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double AveragePrice { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IList<AuctionDTO> Auctions { get; set; }

        public AccessoryPriceDTO()
        {
            ItemName = "";
            Auctions = new List<AuctionDTO>();
        }
    }
}
