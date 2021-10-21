using RetailRentingApp.Backend;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RetailRentingApp.Classes
{
    public class LocationsInGivenTimeIntervalUtils
    {
        public static void FindLocationsInGivenDateInterval(ICollection<RentingOfTradingArea> rentings,
                                                            DatePicker from,
                                                            DatePicker to)
        {
            rentings = rentings.Where(r => (r.StartDate >= from.SelectedDate &&
                    r.EndDate <= to.SelectedDate) ||
                    (r.StartDate <= from.SelectedDate &&
                    r.EndDate >= to.SelectedDate) ||
                    (r.StartDate <= from.SelectedDate &&
                    r.EndDate <= to.SelectedDate) ||
                    (r.StartDate >= from.SelectedDate &&
                    r.EndDate >= to.SelectedDate)).ToList();
        }
    }
}
