using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailRentingApp.Backend
{
    /// <summary>
    /// Public partial class TradingArea.
    /// </summary>
    public partial class TradingArea
    {
        public string IsAirVentingText => IsAirVenting ? "С кондиционером" : "Без кондиционера";
    }
}
