using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using RetailRentingApp.Windows;
using System;
using System.Linq;
using System.Windows;

namespace RetailRentingApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // SimpleDbObjectGenerator.Generate(new TradingAreaDescriptiveGenerator(), 50, AppData.Context);

            DataContext = AppData.Context.Users.ToList();

            PBoxPasswordSecure.Focus();
        }

        /// <summary>
        /// Actions for login into the current app.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = ComboLogin.SelectedItem as User;
            bool userIsSelectedInComboBox = null != currentUser;

            if (userIsSelectedInComboBox)
            {
                if (PasswordIsCorrectFor(currentUser))
                {
                    ShowAllIsOkMessageFor(currentUser);
                    InitializeWindowFor(currentUser);
                    AppData.LoginWindow = this;
                }
                else
                {
                    ShowSomethingWentWrongMessage();
                }
            }
        }

        private bool PasswordIsCorrectFor(User currentUser)
        {
            return PBoxPasswordSecure.Password == currentUser.Password;
        }

        private void InitializeWindowFor(User currentUser)
        {
            FrameHolderWindow fHolderWindow = new FrameHolderWindow
            {
                Owner = this,
            };
            TitleInitalizer.Append(fHolderWindow, currentUser.ToString());
            fHolderWindow.Show();
            AppData.CurrentUser = currentUser;
            Hide();
        }

        private void ShowSomethingWentWrongMessage()
        {
            MessageBox.Show("Неверный пароль. Пожалуйста, введите корректный пароль",
                     "Ошибка авторизации",
                     MessageBoxButton.OK,
                     MessageBoxImage.Warning);
        }

        /// <summary>
        /// Shows the message for user that all is ok.
        /// </summary>
        /// <param name="currentUser"></param>
        private void ShowAllIsOkMessageFor(User currentUser)
        {
            SimpleMessager.ShowInfo("Добро пожаловать, " + currentUser.ToString() + "!");
        }

        /// <summary>
        /// Actions for exiting the current app.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (UserWantsToExitApp())
            {
                App.Current.Shutdown();
            }
        }

        private static bool UserWantsToExitApp()
        {
            return SimpleMessager.ShowQuestion("Точно покинуть приложение?");
        }

        /// <summary>
        /// Resolves the current password visibility state with respect to the CheckBox.
        /// </summary>
        private void CheckPasswordBox_Click(object sender, RoutedEventArgs e)
        {
            if (UserWantsToShowPassword())
            {
                HidePassword();
                DisableLoginButton();
            }
            else
            {
                RevealPassword();
                EnableLoginButton();
            }
        }

        private bool UserWantsToShowPassword()
        {
            return CheckPasswordBox.IsChecked == true;
        }

        private void EnableLoginButton()
        {
            BtnLogin.IsEnabled = true;
        }

        private void DisableLoginButton()
        {
            BtnLogin.IsEnabled = false;
        }

        private void HidePassword()
        {
            TBoxPasswordInsecure.Text = PBoxPasswordSecure.Password;
            PBoxPasswordSecure.Visibility = Visibility.Collapsed;
            TBoxPasswordInsecure.Visibility = Visibility.Visible;
            TBoxPasswordInsecure.Focus();
        }

        private void RevealPassword()
        {
            PBoxPasswordSecure.Password = TBoxPasswordInsecure.Text;
            TBoxPasswordInsecure.Visibility = Visibility.Collapsed;
            PBoxPasswordSecure.Visibility = Visibility.Visible;
            PBoxPasswordSecure.Focus();
        }

        /// <summary>
        /// Goes to the register window.
        /// </summary>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWindow = GetRegistrationWindow();

            regWindow.ShowDialog();
            UpdateLoginBox();

            Hide();
        }

        private RegistrationWindow GetRegistrationWindow()
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            AppData.LoginWindow = this;
            return regWindow;
        }

        /// <summary>
        /// Updates the ComboLogin with new data if it exists.
        /// </summary>
        private void UpdateLoginBox()
        {
            ComboLogin.ItemsSource = AppData.Context.Users.ToList();
        }
    }
}
