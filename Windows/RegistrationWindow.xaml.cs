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
using System.Windows.Shapes;

namespace RetailRentingApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            ComboWorkerType.ItemsSource = AppData.Context.TypeOfUsers.ToList();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            bool wantsToGoBack = SimpleMessager.ShowQuestion("Точно вернуться на окно авторизации?");

            if (wantsToGoBack)
            {
                Close();
                AppData.LoginWindow.Show();
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TBoxFullName.Text))
            {
                errorsBuilder.AppendLine("ФИО не может быть пустым");
            }

            bool isHasAnyErrors = errorsBuilder.Length > 0;

            if (isHasAnyErrors)
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
            }

            string[] credentials = TBoxFullName.Text.Split(' ');

            User user = new User
            {
                LastName = credentials[0],
                MiddleName = credentials[1],
                FirstName = credentials[2],
                TypeOfUser = ComboWorkerType.SelectedItem as TypeOfUser
            };

            AppData.Context.Users.Add(user);

            try
            {
                AppData.Context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError(ex.Message);
            }
        }
    }
}
