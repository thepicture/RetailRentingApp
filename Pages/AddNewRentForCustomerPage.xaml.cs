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
            InitRentingValuesAndDataContext(renting);
        }

        private void InitRentingValuesAndDataContext(RentingOfTradingArea renting)
        {
            _renting.StartDate = _renting.EndDate = DateTime.Now;
            _renting.Renting = renting.Renting;
            _renting.TradingArea = renting.TradingArea;
            DataContext = _renting;
        }

        private void BtnAddRentOfCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (NoErrorsFound())
            {
                AddRentingAreaAndSaveChanges();
            }
        }

        private bool NoErrorsFound()
        {
            StringBuilder errorBuilder = new StringBuilder();
            CheckForInputErrors(errorBuilder);

            if (HasAnyErrors(errorBuilder))
            {
                SimpleMessager.ShowError(errorBuilder.ToString());
                return true;
            }
            return false;
        }

        private void AddRentingAreaAndSaveChanges()
        {
            AddRentingArea();

            TryToSaveChanges();
        }

        private void AddRentingArea()
        {
            _ = AppData.Context.RentingOfTradingAreas.Add(_renting);
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
            SaveAndShowAllIsOkMessage();
            GoToRentsListView();
        }

        private void SaveAndShowAllIsOkMessage()
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo($"Аренда успешно назначена для клиента " +
                $"{_renting.Renting.Customer.CompanyName}!");
        }

        private static void GoToRentsListView()
        {
            AppData.MainFrame.GoBack();
            AppData.MainFrame.GoBack();
        }

        private static bool HasAnyErrors(StringBuilder errorBuilder)
        {
            return errorBuilder.Length > 0;
        }

        private void CheckForInputErrors(StringBuilder errorBuilder)
        {
            if (DatesAreInvalid())
            {
                _ = errorBuilder.AppendLine("Дата начала должна быть строго " +
                    "меньше даты окончания и значения " +
                    "должны быть указаны для " +
                    "даты начала и даты окончания");
            }
        }

        private bool DatesAreInvalid()
        {
            return !DateValidationChecker.IsDateIntervalValid(FromPicker, ToPicker);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            GoToRentsListView();
        }
    }
}
