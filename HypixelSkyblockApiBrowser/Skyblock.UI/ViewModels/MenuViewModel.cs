using Skyblock.UI.Controls.List;
using Skyblock.UI.ViewModels.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Skyblock.UI.ViewModels
{
    public class MenuViewModel : NotifyPropertyChanged
    {
        public BitPricesViewModel BitPricesVM { get; set; }
        public AccessoryPricesViewModel AccessoryPricesVM { get; set; }

        public MenuViewModel(BitPricesViewModel bitPricesVM, AccessoryPricesViewModel accessoryPricesVM)
        {
            BitPricesVM = bitPricesVM;
            AccessoryPricesVM = accessoryPricesVM;
        }

        public void ShowBitPrices()
        {
            Window window = new Window
            {
                Title = "Bit Prices",
                Content = new BitPrices(),
                Height = 600,
                Width = 600,
                DataContext = BitPricesVM
            };

            window.Loaded += async (s, e) => await ((BitPricesViewModel)BitPricesVM).InitializeAsync();

            window.ShowDialog();
        }
        
        public void ShowAccessoryPrices()
        {
            Window window = new Window
            {
                Title = "Accessory Prices",
                Content = new AccessoryPrices(),
                Height = 600,
                Width = 600,
                DataContext = AccessoryPricesVM
            };

            window.Loaded += async (s, e) => await ((AccessoryPricesViewModel)AccessoryPricesVM).InitializeAsync();

            window.ShowDialog();
        }


    }
}
