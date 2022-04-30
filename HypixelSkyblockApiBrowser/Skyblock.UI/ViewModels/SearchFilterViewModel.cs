using Skyblock.Common;
using Skyblock.Common.Domain;
using Skyblock.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.UI.ViewModels
{
    public class SearchFilterViewModel : NotifyPropertyChanged
    {
        public string[] AllRarities { get; set; } = Rarities.List;
        public string[] AllCategories { get; set; } = Categories.List;
        public string[] AllStars { get; set; } = Constants.StarOptions;
        private string itemName;
        public string ItemName { get => itemName; set => Set(ref itemName, value); }
        private string itemLoreContains1;
        public string ItemLoreContains1 { get => itemLoreContains1; set => Set(ref itemLoreContains1, value); }
        private string itemLoreContains2;
        public string ItemLoreContains2 { get => itemLoreContains2; set => Set(ref itemLoreContains2, value); }
        private string itemLoreContains3;
        public string ItemLoreContains3 { get => itemLoreContains3; set => Set(ref itemLoreContains3, value); }
        private string itemLoreDoesNotContain1;
        public string ItemLoreDoesNotContain1 { get => itemLoreDoesNotContain1; set => Set(ref itemLoreDoesNotContain1, value); }
        private string itemLoreDoesNotContain2;
        public string ItemLoreDoesNotContain2 { get => itemLoreDoesNotContain2; set => Set(ref itemLoreDoesNotContain2, value); }
        private bool bin;
        private Rarity selectedRarity;
        public bool Bin { get => bin; set => Set(ref bin, value); }
        private Category selectedCategory;
        public Rarity SelectedRarity { get => selectedRarity; set => Set(ref selectedRarity, value); }
        public Category SelectedCategory { get => selectedCategory; set => Set(ref selectedCategory, value); }
        private string maxPrice;
        public string MaxPrice { get => maxPrice; set => Set(ref maxPrice, value); }
        private string selectedStars;
        public string SelectedStars { get => selectedStars; set => Set(ref selectedStars, value); }

        public SearchFilterViewModel()
        {
            ItemLoreContains1 = "";
            ItemLoreContains2 = "";
            ItemLoreContains3 = "";
            ItemLoreDoesNotContain1 = "";
            ItemLoreDoesNotContain2 = "";
            ItemName = "";
            Bin = true;
            SelectedRarity = Rarity.Any;
            SelectedCategory = Category.Any;
            MaxPrice = "0";
            SelectedStars = Constants.NoStars;
        }
        
        public AuctionQuery ToAuctionQuery()
        {
            return new AuctionQuery()
            {
                ItemName = ItemName,
                Bin = Bin,
                SelectedRarity = SelectedRarity,
                SelectedCategory = SelectedCategory,
                MaxPrice = int.TryParse(MaxPrice, out int res) ? res : 0,
                LoreContains = new List<string>(){ItemLoreContains1, ItemLoreContains2, ItemLoreContains3}.Where(s => s != "").ToList(),
                LoreDoesNotContain = new List<string>(){ItemLoreDoesNotContain1, ItemLoreDoesNotContain2}.Where(s => s != "").ToList(),
                MinimumStars = SelectedStars
            };
        }
    }
}
