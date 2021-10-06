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
            StringBuilder errorsBuilder = CheckForErrors();

            bool isHasAnyErrors = errorsBuilder.Length > 0;

            if (isHasAnyErrors)
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return;
            }
            SaveNewUser();
        }

        private StringBuilder CheckForErrors()
        {
            StringBuilder errorsBuilder = new StringBuilder();
            CheckIfFullNameIsNotEmpty(errorsBuilder);
            CheckIfPasswordsAreCorrect(errorsBuilder);
            return errorsBuilder;
        }

        private void SaveNewUser()
        {
            PrepareCreatingOfUser();
            TryCatchSaveChanges();
        }

        private void PrepareCreatingOfUser()
        {
            string[] credentials = GetCredentials();

            User user = CreateNewUser(credentials);

            AppData.Context.Users.Add(user);
        }

        private void CheckIfPasswordsAreCorrect(StringBuilder errorsBuilder)
        {
            if (string.IsNullOrEmpty(PBoxFirstPassword.Password)
                            || string.IsNullOrEmpty(PBoxPasswordSecond.Password)
                            || PBoxFirstPassword.Password != PBoxPasswordSecond.Password)
            {
                errorsBuilder.AppendLine("Первый пароль должен совпадать со вторым,\n" +
                    "пустые пароли не допускаются");
            }
        }

        private void CheckIfFullNameIsNotEmpty(StringBuilder errorsBuilder)
        {
            if (string.IsNullOrEmpty(TBoxFullName.Text))
            {
                errorsBuilder.AppendLine("ФИО не может быть пустым");
            }
        }

        private void TryCatchSaveChanges()
        {
            try
            {
                TryToSaveChanges();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError(ex.Message);
            }
        }

        private void TryToSaveChanges()
        {
            AppData.Context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены!");
            AbstractClose();
        }

        private User CreateNewUser(string[] credentials)
        {
            return new User
            {
                LastName = credentials[0],
                MiddleName = credentials[1],
                FirstName = credentials[2],
                Password = PBoxFirstPassword.Password,
                TypeOfUser = ComboWorkerType.SelectedItem as TypeOfUser,
                Image = currentImage
            };
        }

        private string[] GetCredentials()
        {
            return TBoxFullName.Text.Split(' ');
        }

        /// <summary>
        /// Appends a new photo to the new user.
        /// </summary>
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (DialogIsFulfilledWithResult(fileDialog))
            {
                AssignImageToUser(fileDialog);
                SimpleMessager.ShowInfo("Фото успешно прикреплено!");
            }
        }

        private void AssignImageToUser(OpenFileDialog fileDialog)
        {
            string path = fileDialog.FileName;
            ImageSource photo = new BitmapImage(new Uri(path));
            currentImage = File.ReadAllBytes(path);
            UserImage.Source = photo;
        }

        private static bool DialogIsFulfilledWithResult(OpenFileDialog fileDialog)
        {
            return (bool)fileDialog.ShowDialog() == true;
        }
    }
}
