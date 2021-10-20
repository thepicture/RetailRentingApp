using RetailRentingApp.Classes;
using System.Windows;
using System.Windows.Controls;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Interaction logic for MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Page
    {
        public MainUserPage()
        {
            InitializeComponent();
        }

        private void BtnFreeRetailLocations_Click(object sender, RoutedEventArgs e)
        {
            OpenFreeRetailWindow();
        }

        private void OpenFreeRetailWindow()
        {
            _ = AppData.MainFrame.Navigate(new FreeTradingLocationsPage());
        }
    }
}
