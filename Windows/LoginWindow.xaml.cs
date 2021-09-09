using RetailRentingApp.Classes;
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

            ComboLogin.ItemsSource = AppData.Context.User.ToList();
        }

        /// <summary>
        /// Actions for login into the current app.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

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
        }

        private void RevealPassword()
        {
            PBoxPasswordSecure.Password = TBoxPasswordInsecure.Text;
            TBoxPasswordInsecure.Visibility = Visibility.Collapsed;
            PBoxPasswordSecure.Visibility = Visibility.Visible;
        }
    }
}
