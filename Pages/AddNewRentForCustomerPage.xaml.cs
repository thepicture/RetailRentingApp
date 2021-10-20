using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for AddNewRentForCustomerPage.xaml
    /// </summary>
    public partial class AddNewRentForCustomerPage : Page
    {
        public RentingOfTradingArea _renting = new RentingOfTradingArea();
        public AddNewRentForCustomerPage(RentingOfTradingArea renting)
        {
            InitializeComponent();

            _renting.StartDate = _renting.EndDate = DateTime.Now;
            _renting.Renting = renting.Renting;
            _renting.TradingArea = renting.TradingArea;

            DataContext = _renting;
        }

        private void BtnAddRentOfCustomer_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorBuilder = new StringBuilder();
            CheckForInputErrors(errorBuilder);

            if (HasAnyErrors(errorBuilder))
            {
                SimpleMessager.ShowError(errorBuilder.ToString());
                return;
            }

            _ = AppData.Context.RentingOfTradingAreas.Add(_renting);

            TryToSaveChanges();
        }

        private void TryToSaveChanges()
        {
            try
            {
                AddNewRentingAndShowMessage();
            }
            catch (Exception)
            {
                SimpleMessager.ShowError("Не удалось добавить новую аренду. " +
                    "Пожалуйста, попробуйте ещё раз");
            }
        }

        private void AddNewRentingAndShowMessage()
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo($"Аренда успешно назначена для клиента " +
                $"{_renting.Renting.Customer.CompanyName}!");
            AppData.MainFrame.GoBack();
            AppData.MainFrame.GoBack();
        }

        private static bool HasAnyErrors(StringBuilder errorBuilder)
        {
            return errorBuilder.Length > 0;
        }

        private void CheckForInputErrors(StringBuilder errorBuilder)
        {
            if (FromPicker.SelectedDate == null)
            {
                _ = errorBuilder.AppendLine("Укажите дату начала");
            }

            if (ToPicker.SelectedDate == null)
            {
                _ = errorBuilder.AppendLine("Укажите дату окончания");
            }

            if (FromPicker.SelectedDate >= ToPicker.SelectedDate)
            {
                _ = errorBuilder.AppendLine("Дата начала должна быть строго меньше даты окончания");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
            AppData.MainFrame.GoBack();
        }
    }
}
