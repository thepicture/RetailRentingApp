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
    }
}
