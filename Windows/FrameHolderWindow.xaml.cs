using RetailRentingApp.Classes;
using RetailRentingApp.Pages;
using System.Windows;

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

            MainFrame.Navigate(new MainUserPage());
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
            if (AppData.MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
