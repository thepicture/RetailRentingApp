using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Collections.Generic;
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
        private List<TradingArea> currentTradingLocations = new List<TradingArea>();
        private ListViewOverwhelmer<TradingArea> overwhelmer;
        public TradingLocationsPage()
        {
            InitializeComponent();
            LViewTradingLocations.ItemsSource = AppData.Context.TradingAreas.ToList();
        }
        private void UpdateListView()
        {
            LViewTradingLocations.Items.Clear();
            currentTradingLocations.Clear();
            currentTradingLocations.AddRange(AppData.Context.TradingAreas.ToList());

            FilterTradingAreas();

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
        }

        private void FilterTradingAreas()
        {
            throw new NotImplementedException();
        }

        private void InitSingletoneOverwhelmer()
        {
            if (overwhelmer == null)
            {
                overwhelmer = new ListViewOverwhelmer<TradingArea>(LViewTradingLocations,
                                                                  currentTradingLocations);
            }
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
            _ = AppData.MainFrame.Navigate(new AddEditTradingAreaPage(null));
        }

        private void TradingLocationsPage__IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                LViewTradingLocations.ItemsSource = AppData.Context.TradingAreas.ToList();
            }
        }
    }
}
