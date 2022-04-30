using System;
using System.Collections.Generic;
using Skyblock.Common.Domain;

namespace Skyblock.Logic.Helper
{
    class Accessories
    {
        public static IList<AccessoryItem> Items { get; set; } = new List<AccessoryItem>
        {
            new("Hegemony Artifact"),
            new("Catacombs Expert Ring"),
            new("Auto Recombobulator"),
            new("Reaper Orb"),
            new("Handy Blood Chalice"),
            new("Nether Artifact"),
            new("Bingo Talisman"),
            new("Ender Artifact"),
            new("Wither Artifact"),
            new("Shady Ring"),
            new("Hunter Talisman"),
            new("Crab Hat of Celebration"),
            new("Legendary Ring of Love"),
            new("Treasure Artifact"),
            new("Beastmaster Crest", Rarity.Epic),
            new("Beastmaster Crest", Rarity.Legendary),
            new("Bat Person Artifact"),
            new("Wither Relic"),
            new("Golden Jerry Artifact"),
            new("Purple Jerry Talisman"),
            new("Master Skull - Tier 4"),
            new("Master Skull - Tier 5"),
            new("Master Skull - Tier 6"),
            new("Master Skull - Tier 7"),
            new("Soulflow Battery"),
            new("Soulflow Supercell"),
            new("Ender Relic"),
            new("Bingo Ring"),
            new("Crooked Artifact"),
            new("Seal of the Family"),
            new("Hunter Ring")
        };

        internal class AccessoryItem
        {
            public string ItemName { get; set; }
            public Rarity Rarity { get; set; }
            public AccessoryItem(string itemName, Rarity rarity = Rarity.Any)
            {
                ItemName = itemName;
                Rarity = rarity;
            }
            public override string ToString()
            {
                return $"{ItemName}";
            }
        }
    }
}
