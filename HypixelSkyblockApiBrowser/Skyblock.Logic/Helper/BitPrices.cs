using System;
using System.Collections.Generic;

namespace Skyblock.Logic.Helper
{
    class BitPrices
    {
        public static IList<BitItem> Items { get; set; } = new List<BitItem>
        {
            new("God Potion", 1_500, ""),
            new("Kat Flower", 500, ""),
            new("Heat Core", 3_000, ""),
            new("Hyper Catalyst Upgrade", 300, ""),
            new("Ultimate Carrot Candy Upgrade", 8_000, ""),
            new("Colossal Experience Bottle Upgrade", 1_200, ""),
            new("Jumbo Backpack Upgrade", 4_000, ""),
            new("Minion Storage X-pender", 1_500, ""),
            new("Hologram", 2_000, ""),
            //new("Dungeon Sack", 10_000, ""),
            new("Builder's Wand", 12_000, ""),
            new("Block Zapper", 5_000, ""),
            new("Bits Talisman", 15_000, ""),
            //new("Rune Sack", 10_000, ""),
            new("Autopet Rules 2-Pack", 21_000, ""),
            new("Kismet Feather", 1_350, ""),
            new("Enchanted Book", 4_000, "Expertise I"),
            new("Enchanted Book", 4_000, "Compact I"),
            new("Enchanted Book", 4_000, "Cultivating I"),
            new("Speed Enrichment", 5_000, ""),
            new("Intelligence Enrichment", 5_000, ""),
            new("Critical Damage Enrichment", 5_000, ""),
            new("Critical Chance Enrichment", 5_000, ""),
            new("Strength Enrichment", 5_000, ""),
            new("Defense Enrichment", 5_000, ""),
            new("Health Enrichment", 5_000, ""),
            new("Magic Find Enrichment", 5_000, ""),
            new("Ferocity Enrichment", 5_000, ""),
            new("Sea Creature Chance Enrichment", 5_000, ""),
            new("Attack Speed Enrichment", 5_000, ""),
            new("Accessory Enrichment Swapper", 200, "")
        };

        internal class BitItem
        {
            public string ItemName { get; set; }
            public int BitPrice { get; set; }
            public string ItemLore { get; set; }
            public BitItem(string itemName, int price, string itemLore)
            {
                ItemName = itemName;
                BitPrice = price;
                ItemLore = itemLore;
            }
            public override string ToString()
            {
                return $"{ItemName} {ItemLore}";
            }
        }
    }
}
