using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
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

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for FreeTradingLocationsPage.xaml
    /// </summary>
    public partial class FreeTradingLocationsPage : Page
    {
        private List<TradingArea> currentTradingLocations = new List<TradingArea>();
        public FreeTradingLocationsPage()
        {
            InitializeComponent();

            UpdateListView();
        }

        private void UpdateListView()
        {
            currentTradingLocations = AppData.Context.TradingAreas.ToList();

            if (FromPicker.SelectedDate != null && ToPicker.SelectedDate != null
                & FromPicker.SelectedDate < ToPicker.SelectedDate)
            {
                //currentTradingLocations = currentTradingLocations.Where(l =>
                //{
                //    var currentRentings = currentTradingLocations.Where(l => l.Renting.Where(e => e.StartDate >= FromPicker.SelectedDate
                //    && e.EndDate <= ToPicker.SelectedDate);

                //    return currentRentings.Count() > 0;
                //}));
            }

            LViewTradingAreas.ItemsSource = currentTradingLocations;
        }

        private void FromPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }

        private void ToPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }

        private void BtnClearDates_Click(object sender, RoutedEventArgs e)
        {
            ToPicker.SelectedDate = FromPicker.SelectedDate = null;
            UpdateListView();
        }
    }
}
