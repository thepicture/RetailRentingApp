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
        }

        private void InitChart()
        {
            ChartRentsUsability.ChartAreas
                            .Add(new ChartArea("RentsUsability"));

            Series series = new Series("MainSeries")
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
            foreach(RentingOfTradingArea area  in areas)
            {
                _ = chartSeries.Points.AddXY(area.Renting.Customer.CompanyName + ", " + area.TradingArea.Name, 20);
            }
        }
    }
}
