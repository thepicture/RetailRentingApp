using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for FreeTradingLocationsPage.xaml
    /// </summary>
    public partial class FreeTradingLocationsPage : Page
    {
        private readonly TimeSpan UPDATE_INTERVAL = TimeSpan.FromMilliseconds(500);
        private List<RentingOfTradingArea> currentRentings = new List<RentingOfTradingArea>();
        public FreeTradingLocationsPage()
        {
            InitializeComponent();
            UpdateListView();
        }

        private void UpdateListView()
        {
            LViewTradingAreas.Items.Clear();
            currentRentings = AppData.Context.RentingOfTradingAreas.ToList();

            CheckDateAndFindLocations();
            RemoveLocationsWithRent();

            OverwhelmListView();
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

        /// <summary>
        /// Non-blocking method for ListView filling with the items.
        /// </summary>
        private void OverwhelmListView()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = UPDATE_INTERVAL
            };

            timer.Start();
            timer.Tick += TimerForListView;
        }

        private void TimerForListView(object sender, EventArgs e)
        {
            bool hasAnyMoreAreas = currentRentings.Count != 0;

            if (!hasAnyMoreAreas)
            {
                DispatcherTimer timer = (DispatcherTimer)sender;

                timer.Stop();

                return;
            }

            AddAnotherArea();
        }

        private void AddAnotherArea()
        {
            RentingOfTradingArea renting = currentRentings[0];
            currentRentings = currentRentings.Skip(1).ToList();
            _ = LViewTradingAreas.Items.Add(renting);
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
    }
}
