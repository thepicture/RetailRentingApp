using System.Windows;

namespace RetailRentingApp.Classes
{
    class TitleInitalizer
    {
        public static void Append(Window window, string appendText)
        {
            window.Title += " - " + appendText;
        }
    }
}
