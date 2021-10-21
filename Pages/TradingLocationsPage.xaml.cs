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
        }
        private void UpdateListView()
        {
            if (LViewTradingLocations == null)
            {
                return;
            }

            LViewTradingLocations.Items.Clear();
            currentTradingLocations.Clear();
            currentTradingLocations.AddRange(AppData.Context.TradingAreas.ToList());

            FilterTradingAreas();

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
        }

        private void FilterTradingAreas()
        {
            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                currentTradingLocations = currentTradingLocations
                    .Where(t => t.Name.ToLower()
                    .Contains(NameBox.Text.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(FloorBox.Text) &&
                int.TryParse(FloorBox.Text, out _))
            {
                currentTradingLocations = currentTradingLocations
                   .Where(t => t.Floor == int.Parse(FloorBox.Text)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(MinAreaInSquareMeters.Text) &&
                !string.IsNullOrWhiteSpace(MaxAreaInSquareMeters.Text) &&
                int.TryParse(MinAreaInSquareMeters.Text, out _) &&
                int.TryParse(MaxAreaInSquareMeters.Text, out _))
            {
                currentTradingLocations = currentTradingLocations
                    .Where(t => t.AreaInSquareMeters < int.Parse(MaxAreaInSquareMeters.Text) &&
                    t.AreaInSquareMeters > int.Parse(MinAreaInSquareMeters.Text)).ToList();
            }

            currentTradingLocations = currentTradingLocations
                .Where(t => t.IsAirVenting == IsAirVenting.IsChecked)
                .ToList();
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
            Button button = sender as Button;
            TradingArea deletingArea = button.DataContext as TradingArea;

            if (SimpleMessager.ShowQuestion($"Точно удалить {deletingArea.Name}?"))
            {
                _ = AppData.Context.TradingAreas.Remove(deletingArea);
                try
                {
                    _ = AppData.Context.SaveChanges();
                    SimpleMessager.ShowInfo($"Торговая точка {deletingArea.Name} " +
                        $"успешно удалена!");
                    UpdateListView();

                }
                catch (Exception ex)
                {
                    SimpleMessager.ShowError("Не удалось удалить торговую точку. " +
                        "Пожалуйста, попробуйте ещё раз. " +
                        ex.Message);
                }
            }
        }

        private void BtnClearFiltration_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = null;
            FloorBox.Text = null;
            MinAreaInSquareMeters.Text = null;
            MaxAreaInSquareMeters.Text = null;
            IsAirVenting.IsChecked = true;
            UpdateListView();
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
                UpdateListView();
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void FloorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void MinAreaInSquareMeters_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void MaxAreaInSquareMeters_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void IsAirVenting_Checked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        private void IsAirVenting_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }
    }
}
