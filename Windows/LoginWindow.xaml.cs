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

            ComboLogin.ItemsSource = AppData.Context.Users.ToList();

            PBoxPasswordSecure.Focus();
        }

        /// <summary>
        /// Actions for login into the current app.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ComboLogin.SelectedItem is User currentUser)
            {
                if (PBoxPasswordSecure.Password == currentUser.Password)
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
            if (MessageBox.Show("Точно покинуть приложение?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        /// <summary>
        /// Resolves the current password visibility state with respect to the CheckBox.
        /// </summary>
        private void CheckPasswordBox_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPasswordBox.IsChecked == true)
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
            Hide();

            RegistrationWindow regWindow = new RegistrationWindow();
            AppData.LoginWindow = this;

            regWindow.ShowDialog();

            UpdateLoginBox();
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
