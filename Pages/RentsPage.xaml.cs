using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for RentsPage.xaml
    /// </summary>
    public partial class RentsPage : Page
    {
        private readonly List<RentingOfTradingArea> currentRentings = new List<RentingOfTradingArea>();
        private ListViewOverwhelmer<RentingOfTradingArea> overwhelmer;
        public RentsPage()
        {
            InitializeComponent();
            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            ComboCustomer.ItemsSource = AppData.Context.Customers.ToList();
            AssignAppropriateRentingsForUser();
        }

        private void AssignAppropriateRentingsForUser()
        {
            ComboRenting.ItemsSource = AppData
                .Context
                .RentingOfTradingAreas
                .ToList()
                .Where(r => r.Renting.ClientId !=
                (ComboCustomer.SelectedItem as Customer)
                .Id);
        }

        private void RentsPageView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (PageIsVisible())
            {
                ReloadAndInfluteLView();
            }
        }

        private bool PageIsVisible()
        {
            return Visibility == Visibility.Visible;
        }

        private void UpdateListView()
        {
            if (LViewIsNotInitialized())
            {
                return;
            }
            InitRents();
        }

        private bool LViewIsNotInitialized()
        {
            return LViewCustomerRentings == null;
        }

        private void InitRents()
        {
            UpdateLViewItems();
            InitSingletoneOverwhelmer();
            overwhelmer.Overwhelm();
        }

        private void UpdateLViewItems()
        {
            LViewCustomerRentings.Items.Clear();
            currentRentings.Clear();
            currentRentings.AddRange(AppData.Context.RentingOfTradingAreas.ToList());
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
            Button button = sender as Button;
            object buttonContext = button.DataContext;
            RentingOfTradingArea deletingArea = buttonContext as RentingOfTradingArea;
            TryToDeleteArea(deletingArea);
        }

        private void TryToDeleteArea(RentingOfTradingArea deletingArea)
        {
            if (IsUserWantsToDeleteArea(deletingArea))
            {
                DeleteFromDbContext(deletingArea);
                TryToSaveChanges();
            }
        }

        private void TryToSaveChanges()
        {
            try
            {
                SaveChangesAndShowFeedback();
                ReloadAndInfluteLView();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError("Не удалось удалить аренду. " +
                    "Пожалуйста, попробуйте ещё раз. Ошибка: " + ex.Message);
            }
        }

        private void ReloadAndInfluteLView()
        {
            AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
            UpdateListView();
        }

        private static void SaveChangesAndShowFeedback()
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo("Аренда торговой точки "
                                    + "успешно удалена!");
        }

        private static void DeleteFromDbContext(RentingOfTradingArea deletingArea)
        {
            AppData.Context.Entry(deletingArea).State = System.Data
                .Entity.EntityState.Deleted;
        }

        private static bool IsUserWantsToDeleteArea(RentingOfTradingArea deletingArea)
        {
            return SimpleMessager.ShowQuestion("Действительно удалить "
                                               + "аренду для торговой точки "
                                               + $"{deletingArea.TradingArea.Name}?");
        }

        private void BtnSaveRenting_Click(object sender, RoutedEventArgs e)
        {
            if (NoErrorsFound())
            {
                SaveTradingArea();
            }
        }

        private bool NoErrorsFound()
        {
            StringBuilder errorsBuilder = new StringBuilder();
            CheckValuesForValidity(errorsBuilder);

            if (HasAnyErrors(errorsBuilder))
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return true;
            }
            return false;
        }

        private static bool HasAnyErrors(StringBuilder errorsBuilder)
        {
            return errorsBuilder.Length > 0;
        }

        private void CheckValuesForValidity(StringBuilder errorsBuilder)
        {
            CheckCustomersComboBox(errorsBuilder);
            CheckRentingsComboBox(errorsBuilder);
            CheckIfDatesAreValid(errorsBuilder);
        }

        private void SaveTradingArea()
        {
            RentingOfTradingArea rentingOfTradingArea = new RentingOfTradingArea
            {
                Renting = (ComboRenting.SelectedItem as RentingOfTradingArea).Renting,
                StartDate = (DateTime)FromPicker.SelectedDate,
                EndDate = (DateTime)ToPicker.SelectedDate,
                TradingArea = (ComboRenting.SelectedItem as RentingOfTradingArea).TradingArea
            };

            _ = AppData.Context.RentingOfTradingAreas.Add(rentingOfTradingArea);
            TryToUpdateDbContext();
        }

        private void TryToUpdateDbContext()
        {
            try
            {
                AddRentAndShowFeedback();
                ReloadAndInfluteLView();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError("Не удалось добавить аренду. " +
                    "Пожалуйста, попробуйте ещё раз. Ошибка: " + ex.Message);
            }
        }

        private static void AddRentAndShowFeedback()
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo("Аренда успешно добавлена!");
        }

        private void CheckIfDatesAreValid(StringBuilder errorsBuilder)
        {
            if (DateIsInvalid())
            {
                _ = errorsBuilder.AppendLine("Укажите корректные даты начала и окончания");
            }
        }

        private bool DateIsInvalid()
        {
            return FromPicker.SelectedDate is null ||
                            ToPicker.SelectedDate is null ||
                            FromPicker.SelectedDate >= ToPicker.SelectedDate;
        }

        private void CheckRentingsComboBox(StringBuilder errorsBuilder)
        {
            if (ComboRenting.SelectedItem is null)
            {
                _ = errorsBuilder.AppendLine("Укажите аренду для клиента");
            }
        }

        private void CheckCustomersComboBox(StringBuilder errorsBuilder)
        {
            if (ComboCustomer.SelectedItem is null)
            {
                _ = errorsBuilder.AppendLine("Укажите клиента для аренды");
            }
        }

        private void ComboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssignAppropriateRentingsForUser();
        }
    }
}
