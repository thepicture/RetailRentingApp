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
            _renting = renting;
            FullfillComboCustomers();
        }

        private void FullfillComboCustomers()
        {
            InsertComboCustomers();
            SelectItemForComboCustomer();
        }

        private void SelectItemForComboCustomer()
        {
            ComboCustomers.SelectedItem = AppData
                            .Context
                            .RentingOfTradingAreas
                            .Find(_renting.Id).Renting.Customer;
        }

        private void InsertComboCustomers()
        {
            ComboCustomers.ItemsSource = AppData
                            .Context
                            .Customers.ToList();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
        }

        private void BtnAddContract_Click(object sender, RoutedEventArgs e)
        {
            bool noCustomerIsSpecified = ComboCustomers.SelectedItem is null;

            if (noCustomerIsSpecified)
            {
                ShowNoClientSpecifiedError();
                return;
            }

            UpdateCustomerAndSaveChanges();
        }

        private void UpdateCustomerAndSaveChanges()
        {
            UpdateCustomerOfRenting();
            TryToSaveChanges();
        }

        private static void TryToSaveChanges()
        {
            try
            {
                SaveChangesAndGoBack();
            }
            catch (Exception)
            {
                ShowCannotRentError();
            }
        }

        private void UpdateCustomerOfRenting()
        {
            AppData.Context.Entry(_renting)
                .Entity
                .Renting.Customer = ComboCustomers
                                    .SelectedItem as Customer;
        }

        private static void ShowCannotRentError()
        {
            SimpleMessager.ShowError("Не удалось назначить аренду клиенту. " +
                                    "Пожалуйста, попробуйте ещё раз.");
        }

        private static void SaveChangesAndGoBack()
        {
            _ = AppData.Context.SaveChanges();
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
