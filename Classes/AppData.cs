using RetailRentingApp.Backend;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// Application data which manages all of the backend context.
    /// </summary>
    class AppData
    {
        public static RetailRentingBaseEntities Context = new RetailRentingBaseEntities();
        public static User CurrentUser = new User();
    }
}
