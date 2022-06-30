using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Skyblock.Common;
using Skyblock.Common.Domain;
using Skyblock.Common.Helper;

namespace Skyblock.Logic.Helper
{
    internal static class Accessories
    {
        static Accessories()
        {
            string? accessoriesData = AppDataIO.TryReadText(Constants.AccessoriesPath);
            if (accessoriesData is null)
            {
                throw new ApplicationException($"Could not parse contents of {Constants.AccessoriesPath}.");
            }
            var items = JsonConvert.DeserializeObject<List<AccessoryItem>>(accessoriesData);
            if (items is null)
            {
                throw new ApplicationException($"Could not deserialize contents of {Constants.AccessoriesPath}.");
            }
            Items = items;
        }
        public static IList<AccessoryItem> Items { get; set; } = new List<AccessoryItem>();
        /*
        public static IList<AccessoryItem> Items { get; set; } = new List<AccessoryItem>
        {
            new("Hegemony Artifact"),
            new("Catacombs Expert Ring"),
            new("Auto Recombobulator"),
            new("Reaper Orb"),
            new("Nether Artifact"),
            new("Bingo Talisman"),
            new("Odger's Bronze Tooth"),
            new("Burststopper Talisman"),
            new("Pulse Ring"),
            new("Netherrack-Looking Sunshade"),
            new("Shady Ring"),
            new("Ender Artifact"),
            new("Wither Artifact"),
            new("Crab Hat Of Celebration"),
            new("Legendary Ring of Love"),
            new("Treasure Artifact"),
            new("Beastmaster Crest", Rarity.Epic),
            new("Beastmaster Crest", Rarity.Legendary),
            new("Bat Person Artifact"),
            new("Wither Relic"),
            new("Purple Jerry Talisman"),
            new("Golden Jerry Artifact"),
            new("Master Skull - Tier 4"),
            new("Master Skull - Tier 5"),
            new("Master Skull - Tier 6"),
            new("Master Skull - Tier 7"),
            new("Soulflow Supercell"),
            new("Ender Relic"),
            new("Artifact of Power"),
            new("Bingo Ring"),
            new("Bingo Artifact"),
            new("Odger's Silver Tooth"),
            new("Odger's Gold Tooth"),
            new("Odger's Diamond Tooth"),
            new("Burststopper Artifact"),
            new("Crooked Artifact"),
            new("Seal of the Family")
        };
        */

        internal class AccessoryItem
        {
            public string ItemName { get; set; }
            public Rarity Rarity { get; set; }
            public bool NameIsExact { get; set; }
            public AccessoryItem(string itemName, Rarity rarity = Rarity.Any, bool nameIsExact = false)
            {
                ItemName = itemName;
                Rarity = rarity;
                NameIsExact = nameIsExact;
            }
            public override string ToString()
            {
                return $"{ItemName}";
            }
        }
    }
}
