using Skyblock.Common.Domain;
using Skyblock.Logic.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.UI.ViewModels.List
{
    public class AuctionFilterViewModel : NotifyPropertyChanged
    {
        public Auction? SelectedAuction { get => selectedAuction; set => Set(ref selectedAuction, value); }
        public IList<Auction>? Auctions { get; set; }
        private readonly AuctionFilterLogicWPF _logic;
        private ObservableCollection<Auction>? filteredAuctions;
        private Auction? selectedAuction;

        public ObservableCollection<Auction>? FilteredAuctions { get => filteredAuctions; set => Set(ref filteredAuctions, value); }

        public AuctionFilterViewModel(AuctionFilterLogicWPF logic)
        {
            _logic = logic;
        }
        public async Task RefreshList(AuctionQuery query)
        {
            if (Auctions is not null)
                FilteredAuctions = new ObservableCollection<Auction>(await _logic.FilterAuctionsAsync(query, Auctions));
        }

        public void CopyAuctionToClipboard()
        {
            if (SelectedAuction is not null)
                System.Windows.Clipboard.SetText(SelectedAuction?.ViewAuction() ?? "");
        }
    }
}
