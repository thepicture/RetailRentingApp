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
            if (overwhelmer == null)
            {
                overwhelmer = new ListViewOverwhelmer<RentingOfTradingArea>(LViewTradingAreas,
                                                                            currentRentings);
            }
        }

        private void UpdateListView()
        {
            LViewTradingAreas.Items.Clear();
            currentRentings.Clear();
            currentRentings.AddRange(AppData.Context.RentingOfTradingAreas.ToList());

            CheckDateAndFindLocations();
            RemoveLocationsWithRent();

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
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
            if (DateIsValid())
            {
                FindLocationsInGivenDateInterval();
            }
        }

        private void FindLocationsInGivenDateInterval()
        {
            currentRentings = currentRentings.Where(r => (r.StartDate >= FromPicker.SelectedDate &&
                    r.EndDate <= ToPicker.SelectedDate) ||
                    (r.StartDate <= FromPicker.SelectedDate &&
                    r.EndDate >= ToPicker.SelectedDate) ||
                    (r.StartDate <= FromPicker.SelectedDate &&
                    r.EndDate <= ToPicker.SelectedDate) ||
                    (r.StartDate >= FromPicker.SelectedDate &&
                    r.EndDate >= ToPicker.SelectedDate)).ToList();
        }

        private bool DateIsValid()
        {
            return FromPicker.SelectedDate != null && ToPicker.SelectedDate != null
                            & FromPicker.SelectedDate < ToPicker.SelectedDate;
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
