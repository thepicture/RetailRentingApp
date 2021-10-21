using RetailRentingApp.Backend;
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
    /// Логика взаимодействия для AddEditTradingAreaPage.xaml
    /// </summary>
    public partial class AddEditTradingAreaPage : Page
    {
        public TradingArea _tradingArea = new TradingArea();
        public AddEditTradingAreaPage(TradingArea tradingArea)
        {
            InitializeComponent();
            SetDataContext(tradingArea);
        }

        private void SetDataContext(TradingArea tradingArea)
        {
            InitTradingArea(tradingArea);
            DataContext = _tradingArea;
        }

        private void InitTradingArea(TradingArea tradingArea)
        {
            if (tradingArea != null)
            {
                _tradingArea = tradingArea;
            }
        }
    }
}
