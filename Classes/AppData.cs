using RetailRentingApp.Backend;
using System.Windows.Controls;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// Application data which manages all of the backend context.
    /// </summary>
    public class AppData
    {
        private static RetailRentingBaseEntities _context;
        public static RetailRentingBaseEntities Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new RetailRentingBaseEntities();
                }

                return _context;
            }
        }
        public static User CurrentUser = new User();
        public static Frame MainFrame { get; set; }
        public static LoginWindow LoginWindow { get; set; }
    }
}
