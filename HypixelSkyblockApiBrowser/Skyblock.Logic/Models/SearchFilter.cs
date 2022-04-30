using Skyblock.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Logic.Models
{
    public class SearchFilter
    {
        public string ItemName { get; set; }
        public string ItemLore { get; set; }
        public bool Bin { get; set; }
        public Rarity SelectedRarity { get; set; }
        public Category SelectedCategory { get; set; }
        public double MaxPrice { get; set; }
    }
}
