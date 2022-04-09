using Skyblock.UI.ViewModels.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Skyblock.UI.Controls.List
{
    /// <summary>
    /// Interaction logic for AuctionFilter.xaml
    /// </summary>
    public partial class AuctionFilter : UserControl
    {
        public AuctionFilter()
        {
            InitializeComponent();
        }

        public void Row_Click(object sender, RoutedEventArgs args)
        {
            ((AuctionFilterViewModel)DataContext).CopyAuctionToClipboard();
        }
    }
}
