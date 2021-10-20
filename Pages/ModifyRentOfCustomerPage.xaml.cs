using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for ModifyRentOfCustomerPage.xaml
    /// </summary>
    public partial class ModifyRentOfCustomerPage : Page
    {
        private readonly RentingOfTradingArea _renting;
        public ModifyRentOfCustomerPage(RentingOfTradingArea renting)
        {
            InitializeComponent();
            _renting = renting;
            FullfillComboCustomers();
        }

        private void FullfillComboCustomers()
        {
            ComboRentors.ItemsSource = AppData
                .Context
                .Customers.ToList();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
        }

        private void BtnAddRent_Click(object sender, RoutedEventArgs e)
        {
            if (ComboRentors.SelectedItem is null)
            {
                ShowNoClientSpecifiedError();
                return;
            }

            GoToAddRentPage();
        }

        private void GoToAddRentPage()
        {
            throw new NotImplementedException();
        }

        private static void ShowNoClientSpecifiedError()
        {
            SimpleMessager.ShowError("Пожалуйста, укажите клиента " +
                "для добавления " +
                "в выпадающем списке");
        }
    }
}
