using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.Domain
{
    public enum Rarity
    {
        Any,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic,
        Divine,
        Supreme,
        Special,
        Very_Special
    }
    public static class Rarities
    {
        public static readonly string[] List = new string[]
        {
            "Any",
            "Common",
            "Uncommon",
            "Rare",
            "Epic",
            "Legendary",
            "Mythic",
            "Divine",
            "Supreme",
            "Special",
            "Very_Special"
        };
    }
}
