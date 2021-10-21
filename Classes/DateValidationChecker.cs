using System.Windows.Controls;

namespace RetailRentingApp.Classes
{
    public class DateValidationChecker
    {
        public static bool IsDateIntervalValid(DatePicker from, DatePicker to)
        {
            return from.SelectedDate != null && to.SelectedDate != null
                            & from.SelectedDate < to.SelectedDate;
        }
    }
}
