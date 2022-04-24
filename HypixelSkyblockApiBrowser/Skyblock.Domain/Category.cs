using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Domain
{
    public enum Category {
        Any,
        Weapon,
        Armor,
        Accessories,
        Consumables,
        Blocks,
        Misc
    }
    public static class Categories
    {
        public static readonly string[] List = new string[]
        {
            "Any",
            "Weapon",
            "Armor",
            "Accessories",
            "Consumables",
            "Blocks",
            "Misc"
        };
    }
}
