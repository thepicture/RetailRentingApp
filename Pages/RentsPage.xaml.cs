using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for RentsPage.xaml
    /// </summary>
    public partial class RentsPage : Page
    {
        private List<RentingOfTradingArea> currentRentings = new List<RentingOfTradingArea>();
        private ListViewOverwhelmer<RentingOfTradingArea> overwhelmer;
        public RentsPage()
        {
            InitializeComponent();
            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            ComboCustomer.ItemsSource = AppData.Context.Customers.ToList();
            ComboRenting.ItemsSource = currentRentings;
        }

        private void RentsPageView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                UpdateListView();
            }
        }

        private void UpdateListView()
        {
            if (LViewCustomerRentings == null)
            {
                return;
            }

            LViewCustomerRentings.Items.Clear();
            currentRentings.Clear();
            currentRentings.AddRange(AppData.Context.RentingOfTradingAreas.ToList());

            InitSingletoneOverwhelmer();

            overwhelmer.Overwhelm();
        }

        private void InitSingletoneOverwhelmer()
        {
            if (overwhelmer == null)
            {
                overwhelmer = new ListViewOverwhelmer<RentingOfTradingArea>(LViewCustomerRentings,
                                                                  currentRentings);
            }
        }

        private void BtnDeleteRenting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveRenting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
