using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
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
        private readonly RentingOfTradingArea _renting;
        public AddNewRentContractPage(RentingOfTradingArea renting)
        {
            InitializeComponent();
            FullfillComboCustomers();
            _renting = renting;
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
                UpdateCustomerOfRenting(context);
                TryToSaveChanges(context);
            }
        }

        private static void TryToSaveChanges(RetailRentingBaseEntities context)
        {
            try
            {
                SaveChangesAndGoBack(context);
            }
            catch (Exception)
            {
                ShowCannotRentError();
            }
        }

        private void UpdateCustomerOfRenting(RetailRentingBaseEntities context)
        {
            context.Entry(_renting)
                                .Entity
                                .Renting.Customer = ComboCustomers.SelectedItem as Customer;
        }

        private static void ShowCannotRentError()
        {
            SimpleMessager.ShowError("Не удалось назначить аренду клиенту. " +
                                    "Пожалуйста, попробуйте ещё раз.");
        }

        private static void SaveChangesAndGoBack(RetailRentingBaseEntities context)
        {
            _ = context.SaveChanges();
            SimpleMessager.ShowInfo("Торговая точка успешно назначена в аренду клиенту!");
            AppData.MainFrame.GoBack();
        }

        private static void ShowNoClientSpecifiedError()
        {
            SimpleMessager.ShowError("Пожалуйста, укажите клиента " +
                "для добавления " +
                "в выпадающем списке");
        }
    }
}
