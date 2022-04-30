using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.DTOs
{
    public class BitPriceDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string ItemName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int PriceInBits { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int CoinsPerBit { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int ItemCount { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double AveragePrice { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IList<AuctionDTO> Auctions { get; set; }

        public BitPriceDTO()
        {
            ItemName = "";
            Auctions = new List<AuctionDTO>();
        }
    }
}
