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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            AppData.MainFrame.Navigate(new FreeTradingLocationsPage());
        }
    }
}
