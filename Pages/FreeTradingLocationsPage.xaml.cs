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
        private List<TradingArea> currentTradingLocations = new List<TradingArea>();
        public FreeTradingLocationsPage()
        {
            InitializeComponent();
            UpdateListView();
        }

        private void UpdateListView()
        {
            LViewTradingAreas.Items.Clear();
            currentTradingLocations = AppData.Context.TradingAreas.ToList();
            CheckDateAndFindLocations();
            OverwhelmListView();
            RemoveLocationsWithRent();
        }

        private void RemoveLocationsWithRent()
        {
            currentTradingLocations = currentTradingLocations.Where(l =>
            {
                DateTime currentDate = DateTime.Now;
                List<RentingOfTradingArea> locations = l
                .RentingOfTradingAreas
                .Where(r => r.StartDate <= currentDate &&
                       r.EndDate >= currentDate
                      )
                .ToList();

                return locations.Count == 0;
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
            currentTradingLocations = currentTradingLocations.Where(l =>
            {
                List<RentingOfTradingArea> locations = l
                .RentingOfTradingAreas
                .Where(r => (r.StartDate >= FromPicker.SelectedDate &&
                    r.EndDate <= ToPicker.SelectedDate) ||
                    (r.StartDate <= FromPicker.SelectedDate &&
                    r.EndDate >= ToPicker.SelectedDate) ||
                    (r.StartDate <= FromPicker.SelectedDate &&
                    r.EndDate <= ToPicker.SelectedDate) ||
                    (r.StartDate >= FromPicker.SelectedDate &&
                    r.EndDate >= ToPicker.SelectedDate)
                    )
                .ToList();

                return locations.Count == 0;
            }).ToList();
        }

        private bool IsAreaHasNotRent(TradingArea l)
        {
            ICollection<RentingOfTradingArea> rentings = l.RentingOfTradingAreas;

            rentings = rentings
            .Where(r => (r.StartDate >= FromPicker.SelectedDate &&
            r.EndDate <= ToPicker.SelectedDate) ||
            (r.StartDate <= FromPicker.SelectedDate &&
            r.EndDate >= ToPicker.SelectedDate) ||
            (r.StartDate <= FromPicker.SelectedDate &&
            r.EndDate <= ToPicker.SelectedDate) ||
            (r.StartDate >= FromPicker.SelectedDate &&
            r.EndDate >= ToPicker.SelectedDate)
            )
            .ToList();

            return rentings.Count == 0;
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
            bool hasAnyMoreAreas = currentTradingLocations.Count != 0;

            if (!hasAnyMoreAreas)
            {
                ((DispatcherTimer)sender).Stop();

                return;
            }

            AddAnotherArea();
        }

        private void AddAnotherArea()
        {
            TradingArea tradingLocation = currentTradingLocations[0];
            currentTradingLocations = currentTradingLocations.Skip(1).ToList();
            _ = LViewTradingAreas.Items.Add(tradingLocation);
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
            object context = (sender as Button).DataContext;
            TradingArea tradingArea = context as TradingArea;

            AppData.MainFrame.Navigate(new AddNewRentContractPage(tradingArea));
        }
    }
}
