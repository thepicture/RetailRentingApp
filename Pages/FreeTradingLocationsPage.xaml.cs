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

            if (FromPicker.SelectedDate != null && ToPicker.SelectedDate != null
                & FromPicker.SelectedDate < ToPicker.SelectedDate)
            {
                currentTradingLocations = currentTradingLocations.Where(l =>
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
                }).ToList();
            }

            OverwhelmListView();
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
            LViewTradingAreas.Items.Add(tradingLocation);
        }

        private void FromPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => UpdateListView();

        private void ToPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => UpdateListView();

        private void BtnClearDates_Click(object sender, RoutedEventArgs e)
        {
            ToPicker.SelectedDate = FromPicker.SelectedDate = null;
            UpdateListView();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
