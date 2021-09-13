using RetailRentingApp.Backend;
using System.Windows.Controls;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// Application data which manages all of the backend context.
    /// </summary>
    public class AppData
    {
        public static RetailRentingBaseEntities Context = new RetailRentingBaseEntities();
        public static User CurrentUser = new User();
        public static Frame MainFrame { get; set; }
        public static LoginWindow LoginWindow { get; set; }
    }
}
