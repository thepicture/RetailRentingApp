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
            InitAndAddSeries();
        }

        private void InitAndAddSeries()
        {
            Series series = GetInitializedSeries();

            ChartRentsUsability.Series.Add(series);
        }

        private static Series GetInitializedSeries()
        {
            return new Series("Оборот торговых точек")
            {
                IsValueShownAsLabel = true
            };
        }

        private void ChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            Series chartSeries = GetInitializedChart();
            List<RentingOfTradingArea> areas = AppData.Context.RentingOfTradingAreas.ToList();
            areas = GetAreas(areas);
            InfluteChart(chartSeries, areas);
        }

        private static void InfluteChart(Series chartSeries, List<RentingOfTradingArea> areas)
        {
            var mappedAreas = areas.Select(a => new { a.TradingArea.Name })
                .Distinct()
                .ToList();

            foreach (var area in mappedAreas)
            {
                _ = chartSeries.Points.AddXY(area.Name,
                                             areas.Count(a => a.TradingArea.Name.Equals(area.Name)));
            }
        }

        private List<RentingOfTradingArea> GetAreas(List<RentingOfTradingArea> areas)
        {
            bool datesAreValid = DateValidationChecker.IsDateIntervalValid(DatePickerFrom, DatePickerTo);

            if (datesAreValid)
            {
                areas = areas.Where(a => a.StartDate > DatePickerFrom.SelectedDate &&
                a.EndDate < DatePickerTo.SelectedDate).ToList();
            }

            return areas;
        }

        private Series GetInitializedChart()
        {
            SeriesChartType chartType = (SeriesChartType)ComboChartType.SelectedItem;
            Series chartSeries = ChartRentsUsability.Series.First();
            chartSeries.ChartType = chartType;
            chartSeries.Points.Clear();
            return chartSeries;
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
