using Skyblock.Logic;
using Skyblock.UI.ViewModels;
using System.Windows;

namespace Skyblock.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var logic = new AuctionFilterLogicWPF();
            var model = new MainWindowViewModel(logic);
            DataContext = model;
            Loaded += async (s, e) => await model.InitializeAsync();
        }
    }
}
