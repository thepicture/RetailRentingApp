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
        private readonly List<TradingArea> currentTradingLocations = new List<TradingArea>();
        private ListViewOverwhelmer<TradingArea> overwhelmer;
        public TradingLocationsPage()
        {
            InitializeComponent();
        }
        private void UpdateListView()
        {
            if (LViewIsNotInitialized())
            {
                return;
            }

            UpdateCurrentLViewItems();
            FilterTradingAreas();
            InitSingletoneOverwhelmer();
            overwhelmer.Overwhelm();
        }

        private void UpdateCurrentLViewItems()
        {
            LViewTradingLocations.Items.Clear();
            currentTradingLocations.Clear();
            currentTradingLocations.AddRange(AppData.Context.TradingAreas.ToList());
        }

        private bool LViewIsNotInitialized()
        {
            return LViewTradingLocations == null;
        }

        private void FilterTradingAreas()
        {
            CheckForFiltration();
            UpdateLViewItemsAirVentingState();
        }

        private void CheckForFiltration()
        {
            CheckIfNameIsFiltered();
            CheckIfFloorBoxIsFiltered();
            CheckIfAreaIsFiltered();
        }

        private void UpdateLViewItemsAirVentingState()
        {
            _ = currentTradingLocations.RemoveAll(t => t.IsAirVenting !=
                          IsAirVenting.IsChecked && t.IsAirVenting == FalseThatIsAirVenting.IsChecked);
        }

        private void CheckIfAreaIsFiltered()
        {
            if (AreaIsNotInvalid())
            {
                _ = currentTradingLocations.RemoveAll(t => !(t.AreaInSquareMeters <
                 int.Parse(MaxAreaInSquareMeters.Text) &&
                      t.AreaInSquareMeters > int.Parse(MinAreaInSquareMeters.Text)));
            }
        }

        private bool AreaIsNotInvalid()
        {
            return !string.IsNullOrWhiteSpace(MinAreaInSquareMeters.Text) &&
                            !string.IsNullOrWhiteSpace(MaxAreaInSquareMeters.Text) &&
                            int.TryParse(MinAreaInSquareMeters.Text, out _) &&
                            int.TryParse(MaxAreaInSquareMeters.Text, out _);
        }

        private void CheckIfFloorBoxIsFiltered()
        {
            if (IsFloorBoxIsNotInvalid())
            {
                _ = currentTradingLocations.RemoveAll(t => t.Floor !=
                  int.Parse(FloorBox.Text));
            }
        }

        private bool IsFloorBoxIsNotInvalid()
        {
            return !string.IsNullOrWhiteSpace(FloorBox.Text) &&
                            int.TryParse(FloorBox.Text, out _);
        }

        private void CheckIfNameIsFiltered()
        {
            if (IsNameNotInvalid())
            {
                _ = currentTradingLocations.RemoveAll(t => !t.Name.ToLower()
                      .Contains(NameBox.Text.ToLower()));
            }
        }

        private bool IsNameNotInvalid()
        {
            return !string.IsNullOrWhiteSpace(NameBox.Text);
        }

        private void InitSingletoneOverwhelmer()
        {
            if (OverwhelmerIsNotInitialized())
            {
                overwhelmer = new ListViewOverwhelmer<TradingArea>(LViewTradingLocations,
                                                                  currentTradingLocations);
            }
        }

        private bool OverwhelmerIsNotInitialized()
        {
            return overwhelmer == null;
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

            if (UserWantsToDeleteArea(deletingArea))
            {
                SaveStateInDbContext(deletingArea);
            }
        }

        private void SaveStateInDbContext(TradingArea deletingArea)
        {
            RemoveAreaFromDbContext(deletingArea);
            TryToSave(deletingArea);
        }

        private void TryToSave(TradingArea deletingArea)
        {
            try
            {
                SaveChangesAndShowFeedback(deletingArea);
                UpdateListView();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError("Не удалось удалить торговую точку. " +
                    "Пожалуйста, попробуйте ещё раз. " +
                    ex.Message);
            }
        }

        private static void SaveChangesAndShowFeedback(TradingArea deletingArea)
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo($"Торговая точка {deletingArea.Name} " +
                $"успешно удалена!");
        }

        private static void RemoveAreaFromDbContext(TradingArea deletingArea)
        {
            _ = AppData.Context.TradingAreas.Remove(deletingArea);
        }

        private static bool UserWantsToDeleteArea(TradingArea deletingArea)
        {
            return SimpleMessager.ShowQuestion($"Точно удалить {deletingArea.Name}?");
        }

        private void BtnClearFiltration_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = null;
            FloorBox.Text = null;
            MinAreaInSquareMeters.Text = null;
            MaxAreaInSquareMeters.Text = null;
            IsAirVenting.IsChecked = true;
            FalseThatIsAirVenting.IsChecked = true;
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

        private void FalseThatIsAirVenting_Checked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        private void FalseThatIsAirVenting_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }
    }
}
