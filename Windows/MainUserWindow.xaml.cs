using RetailRentingApp.Classes;
using System.Windows;

namespace RetailRentingApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainUserWindow.xaml
    /// </summary>
    public partial class MainUserWindow : Window
    {
        public MainUserWindow()
        {
            InitializeComponent();
        }

        private void BtnFreeRetailLocations_Click(object sender, RoutedEventArgs e)
        {
            OpenFreeRetailWindow();
        }

        private void OpenFreeRetailWindow()
        {
            var freeTradingLocationsWindow = new FreeTradingLocationsWindow
            {
                Owner = this
            };
            freeTradingLocationsWindow.Show();
            Hide();
        }
    }
}
