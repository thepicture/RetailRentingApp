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
    /// Interaction logic for RentTradingLocationsPage.xaml
    /// </summary>
    public partial class RentTradingLocationsPage : Page
    {
        private List<RentingOfTradingArea> currentRentings = new List<RentingOfTradingArea>();
        private ListViewOverwhelmer<RentingOfTradingArea> overwhelmer;
        public RentTradingLocationsPage()
        {
            InitializeComponent();
        }

        private void FromPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }

        private void ToPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListView();
        }

        private void UpdateListView()
        {
            LViewTradingAreas.Items.Clear();
            currentRentings.Clear();
            currentRentings.AddRange(AppData.Context.RentingOfTradingAreas.ToList());

            CheckDateAndFindLocations();
            RemoveLocationsWithoutRent();

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
        }

        private void InitSingletoneOverwhelmer()
        {
            if (overwhelmer == null)
            {
                overwhelmer = new ListViewOverwhelmer<RentingOfTradingArea>(LViewTradingAreas,
                                                                            currentRentings);
            }
        }

        private void RemoveLocationsWithoutRent()
        {
            currentRentings = currentRentings.Where(r =>
            {
                DateTime currentDate = DateTime.Now;

                bool isSatisfies = (r.StartDate <= currentDate) &&
                       (r.EndDate >= currentDate);

                return isSatisfies;
            }).ToList();
        }

        private void CheckDateAndFindLocations()
        {
            if (DateValidationChecker.IsDateIntervalValid(FromPicker, ToPicker))
            {
                LocationsInGivenTimeIntervalUtils.FindLocationsInGivenDateInterval(currentRentings,
                                                                                   FromPicker,
                                                                                   ToPicker);
            }
        }

        private void BtnClearDates_Click(object sender, RoutedEventArgs e)
        {
            ToPicker.SelectedDate = FromPicker.SelectedDate = null;
            UpdateListView();
        }
        private void RentTradingLocations_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                UpdateListView();
            }
        }
    }
}