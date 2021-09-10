using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
