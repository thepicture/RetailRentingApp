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
            Button button = sender as Button;
            object buttonContext = button.DataContext;
            RentingOfTradingArea deletingArea = buttonContext as RentingOfTradingArea;

            bool userWantsToDeleteArea = SimpleMessager
                .ShowQuestion("Действительно удалить " +
                "аренду для торговой точки " +
                            $"{deletingArea.TradingArea.Name}?");

            if (userWantsToDeleteArea)
            {
                AppData.Context.Entry(deletingArea).State = System.Data
                    .Entity.EntityState.Deleted;
                try
                {
                    _ = AppData.Context.SaveChanges();
                    SimpleMessager.ShowInfo("Аренда торговой точки " +
                        "успешно удалена!");
                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                    UpdateListView();
                }
                catch (Exception ex)
                {
                    SimpleMessager.ShowError("Не удалось удалить аренду. " +
                        "Пожалуйста, попробуйте ещё раз. Ошибка: " + ex.Message);
                }
            }
        }

        private void BtnSaveRenting_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsBuilder = new StringBuilder();

            CheckCustomersComboBox(errorsBuilder);
            CheckRentingsComboBox(errorsBuilder);
            CheckIfDatesAreValid(errorsBuilder);

            bool hasAnyErrors = errorsBuilder.Length > 0;

            if (hasAnyErrors)
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return;
            }

            SaveTradingArea();
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
                _ = AppData.Context.SaveChanges();
                SimpleMessager.ShowInfo("Аренда успешно добавлена!");
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                UpdateListView();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError("Не удалось добавить аренду. " +
                    "Пожалуйста, попробуйте ещё раз. Ошибка: " + ex.Message);
            }
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
