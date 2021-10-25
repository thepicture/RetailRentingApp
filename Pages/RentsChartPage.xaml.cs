using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for RentsChartPage.xaml
    /// </summary>
    public partial class RentsChartPage : Page
    {
        public RentsChartPage()
        {
            InitializeComponent();
            InitChart();
            OverwhelmComboChartTypes();
            UpdateChart();
        }

        private void OverwhelmComboChartTypes()
        {
            ComboChartType.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
            ComboChartType.SelectedItem = SeriesChartType.RangeColumn;
        }

        private void InitChart()
        {
            ChartRentsUsability.ChartAreas
                            .Add(new ChartArea("RentsUsability"));

            Series series = new Series("Оборот торговых точек")
            {
                IsValueShownAsLabel = true
            };

            ChartRentsUsability.Series.Add(series);
        }

        private void ChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            SeriesChartType chartType = (SeriesChartType)ComboChartType.SelectedItem;
            Series chartSeries = ChartRentsUsability.Series.First();
            chartSeries.Points.Clear();
            chartSeries.ChartType = chartType;
            List<RentingOfTradingArea> areas = AppData.Context.RentingOfTradingAreas.ToList();

            bool datesAreValid = DateValidationChecker.IsDateIntervalValid(DatePickerFrom, DatePickerTo);

            if (datesAreValid)
            {
                areas = areas.Where(a => a.StartDate > DatePickerFrom.SelectedDate &&
                a.EndDate < DatePickerTo.SelectedDate).ToList();
            }

            var mappedAreas = areas.Select(a => new { a.TradingArea.Name }).Distinct().ToList();

            foreach (var area in mappedAreas)
            {
                _ = chartSeries.Points.AddXY(area.Name,
                                             areas.Count(a => a.TradingArea.Name.Equals(area.Name)));
            }
        }

        private void DatePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void DatePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void BtnClearFiltration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DatePickerFrom.SelectedDate = DatePickerTo.SelectedDate = null;
            UpdateChart();
        }
    }
}
