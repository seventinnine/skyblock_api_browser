using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Skyblock.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Client
{
    public class AuctionData
    {
        public string Item_Name { get; set; }
        public string Short_Name { get; set; }
        public string Item_Lore { get; set; }
        public int Starting_Bid { get; set; }
        public bool Bin { get; set; }
        public string Auctioneer { get; set; }
        public string UUID { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity Tier { get; set; }

        public AuctionData()
        {
            Item_Name = "";
            Short_Name = "";
            Item_Lore = "";
            Auctioneer = "";
            UUID = "";
        }
    }

    public static class AuctionDataExtensions
    {
        public static Auction ToDomain(this AuctionData auctionData)
        {
            string clearName = auctionData.Item_Name.RemoveWeirdCharacters();
            return new Auction
            {
                ItemName = clearName,
                PetLevel = clearName.GetPetLevel(),
                ItemLore = auctionData.Item_Lore.RemoveWeirdCharacters(),
                StartingBid = auctionData.Starting_Bid,
                Bin = auctionData.Bin,
                Auctioneer = auctionData.Auctioneer,
                UUID = auctionData.UUID,
                Tier = auctionData.Tier,
                Category = auctionData.Category,
            };
        }
        public static IList<Auction> ToDomains(this IList<AuctionData> auctionDataList)
        {
            return auctionDataList.Select(ad => ad.ToDomain()).ToList();
        }

        public static string RemoveWeirdCharacters(this string s)
        {
            string res = s;
            int i;
            do
            {
                i = res.IndexOf('§');
                if (i >= 0) res = res[..i] + res[Math.Min(i + 2, res.Length)..];
            } while (i >= 0);
            return res;
        }

        private static readonly string levelString = "[Lvl ";
        public static int GetPetLevel(this string s)
        {
            int exists = s.IndexOf("[Lvl ", StringComparison.CurrentCultureIgnoreCase);
            if (exists >= 0)
            {
                int start = exists + levelString.Length - 1;
                int end = s.IndexOf("]", StringComparison.CurrentCultureIgnoreCase);
                return int.TryParse(s[start..end], out int res) ? res : 0;

            }
            else
            {
                return 0;
            }
        }

    }
}
