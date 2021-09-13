using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// The class, in which the only purpose is to clear context of LoginWindow.
    /// </summary>
    public class LoginWindowUtils
    {
        /// <summary>
        /// The method which can clear context of LoginWindow if the window exists.
        /// </summary>
        public static void ClearContext()
        {
            if (AppData.LoginWindow == null)
            {
                return;
            }

            AppData.LoginWindow.PBoxPasswordSecure.Password = AppData.LoginWindow.TBoxPasswordInsecure.Text = null;
        }
    }
}
