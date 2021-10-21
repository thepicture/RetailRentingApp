using RetailRentingApp.Backend;
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
            Button button = sender as Button;
            TradingArea tradingArea = button.DataContext as TradingArea;

            _ = AppData.MainFrame.Navigate(new AddEditTradingAreaPage(tradingArea));
        }

        private void BtnDeleteTradingArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClearFiltration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddTrading_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new AddEditTradingAreaPage(null));
        }
    }
}
