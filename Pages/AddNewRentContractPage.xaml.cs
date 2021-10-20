using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for AddNewRentContractPage.xaml
    /// </summary>
    public partial class AddNewRentContractPage : Page
    {
        private readonly TradingArea _tradingArea;
        public AddNewRentContractPage(TradingArea tradingArea)
        {
            InitializeComponent();
            FullfillComboCustomers();
            _tradingArea = tradingArea;
        }

        private void FullfillComboCustomers()
        {
            using (RetailRentingBaseEntities context = new RetailRentingBaseEntities())
            {
                ComboCustomers.ItemsSource = context.Customers.ToList();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
        }

        private void BtnAddContract_Click(object sender, RoutedEventArgs e)
        {
            if (ComboCustomers.SelectedItem is null)
            {
                ShowNoClientSpecifiedError();

                return;
            }

            using (RetailRentingBaseEntities context = new RetailRentingBaseEntities())
            {
                _tradingArea.RentingOfTradingAreas.Add(new RentingOfTradingArea
                {

                });
            }
        }

        private static void ShowNoClientSpecifiedError()
        {
            SimpleMessager.ShowError("Пожалуйста, укажите клиента " +
                "для добавления " +
                "в выпадающем списке");
        }
    }
}
