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
    /// Interaction logic for FreeTradingLocationsPage.xaml
    /// </summary>
    public partial class FreeTradingLocationsPage : Page
    {
        private List<RentingOfTradingArea> currentRentings = new List<RentingOfTradingArea>();
        private ListViewOverwhelmer<RentingOfTradingArea> overwhelmer;
        public FreeTradingLocationsPage()
        {
            InitializeComponent();
        }

        private void InitSingletoneOverwhelmer()
        {
            if (OverwhelmerIsNotInitialized())
            {
                overwhelmer = new ListViewOverwhelmer<RentingOfTradingArea>(LViewTradingAreas,
                                                                            currentRentings);
            }
        }

        private bool OverwhelmerIsNotInitialized()
        {
            return overwhelmer == null;
        }

        private void UpdateListView()
        {
            UpdateCurrentItems();

            CheckDateAndFindLocations();
            RemoveLocationsWithRent();

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
        }

        private void UpdateCurrentItems()
        {
            LViewTradingAreas.Items.Clear();
            currentRentings.Clear();
            currentRentings.AddRange(AppData.Context.RentingOfTradingAreas.ToList());
        }

        private void RemoveLocationsWithRent()
        {
            currentRentings = currentRentings.Where(r =>
            {
                DateTime currentDate = DateTime.Now;

                return r.StartDate > currentDate ||
                       r.EndDate < currentDate;
            }).ToList();
        }

        private void CheckDateAndFindLocations()
        {
            if (DatesAreValid())
            {
                FindLocationsInGivenInterval();
            }
        }

        private void FindLocationsInGivenInterval()
        {
            LocationsInGivenTimeIntervalUtils.FindLocationsInGivenDateInterval(currentRentings,
                                                                                               FromPicker,
                                                                                               ToPicker);
        }

        private bool DatesAreValid()
        {
            return DateValidationChecker.IsDateIntervalValid(FromPicker, ToPicker);
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

        private void BtnStartContract_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            object context = button.DataContext;
            RentingOfTradingArea renting = context as RentingOfTradingArea;

            _ = AppData.MainFrame.Navigate(new AddNewRentContractPage(renting));
        }

        private void FreeTradingsPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                UpdateListView();
            }
        }

        private void BtnAddNewRenting_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            object context = button.DataContext;
            RentingOfTradingArea renting = context as RentingOfTradingArea;

            _ = AppData.MainFrame.Navigate(new ModifyRentOfCustomerPage(renting));
        }
    }
}
