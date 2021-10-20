using RetailRentingApp.Classes;
using RetailRentingApp.Pages;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Windows
{
    /// <summary>
    /// Interaction logic for FrameHolderWindow.xaml
    /// </summary>
    public partial class FrameHolderWindow : Window
    {
        public FrameHolderWindow()
        {
            InitializeComponent();

            AppData.MainFrame = MainFrame;

            _ = MainFrame.Navigate(new MainUserPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.MainFrame.CanGoBack)
            {
                AppData.MainFrame.GoBack();
            }
        }

        private void MainFrame_ContentRendered(object sender, System.EventArgs e)
        {
            Title = (MainFrame.Content as Page).Title;

            BtnBack.Visibility = AppData.MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (SimpleMessager.ShowQuestion("Точно завершить сессию и вернуться в окно авторизации?"))
            {
                Close();
                LoginWindowUtils.ClearContext();
                AppData.LoginWindow.Show();
            }
        }
    }
}
