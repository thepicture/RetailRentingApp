using RetailRentingApp.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for TradingLocationsPage.xaml
    /// </summary>
    public partial class TradingLocationsPage : Page
    {
        public TradingLocationsPage()
        {
            InitializeComponent();
            LViewTradingAreas.ItemsSource = AppData.Context.TradingAreas.ToList();
        }

        private void BtnModifyTradingArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteTradingArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClearFiltration_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
