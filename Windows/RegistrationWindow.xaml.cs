using Microsoft.Win32;
using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetailRentingApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private byte[] currentImage = null;
        public RegistrationWindow()
        {
            InitializeComponent();

            ComboWorkerType.ItemsSource = AppData.Context.TypeOfUsers.ToList();
        }

        /// <summary>
        /// Button to go back to the login window.
        /// </summary>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            bool wantsToGoBack = SimpleMessager.ShowQuestion("Точно вернуться на окно авторизации?");

            if (wantsToGoBack)
            {
                AbstractClose();
            }
        }

        private void AbstractClose()
        {
            Close();
            AppData.LoginWindow.Show();
        }

        /// <summary>
        /// Register the new user
        /// or messages the user that input data is incorrect.
        /// </summary>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(TBoxFullName.Text))
            {
                errorsBuilder.AppendLine("ФИО не может быть пустым");
            }

            if (string.IsNullOrEmpty(PBoxFirstPassword.Password)
                || string.IsNullOrEmpty(PBoxPasswordSecond.Password)
                || PBoxFirstPassword.Password != PBoxPasswordSecond.Password)
            {
                errorsBuilder.AppendLine("Первый пароль должен совпадать со вторым,\n" +
                    "пустые пароли не допускаются");
            }

            bool isHasAnyErrors = errorsBuilder.Length > 0;

            if (isHasAnyErrors)
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return;
            }

            string[] credentials = TBoxFullName.Text.Split(' ');

            User user = new User
            {
                LastName = credentials[0],
                MiddleName = credentials[1],
                FirstName = credentials[2],
                Password = PBoxFirstPassword.Password,
                TypeOfUser = ComboWorkerType.SelectedItem as TypeOfUser,
                Image = currentImage
            };

            AppData.Context.Users.Add(user);

            try
            {
                AppData.Context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                AbstractClose();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Appends a new photo to the new user.
        /// </summary>
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if ((bool)fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                ImageSource photo = new BitmapImage(new Uri(path));
                currentImage = File.ReadAllBytes(path);
                UserImage.Source = photo;
                SimpleMessager.ShowInfo("Фото успешно прикреплено!");
            }
        }
    }
}
