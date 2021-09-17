using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// The simple message proxy with some useful methods.
    /// </summary>
    public class SimpleMessager
    {
        /// <summary>
        /// The simple proxy for MessageBox class to simplify error statements.
        /// </summary>
        /// <param name="message">An error message for user.</param>
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// The simple proxy for MessageBox class to simplify "if" statements.
        /// </summary>
        /// <param name="message">An asking message for user.</param>
        /// <returns>The bool result of user's choice.</returns>
        public static bool ShowQuestion(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Shows an message information to the user.
        /// </summary>
        /// <param name="message">An information message for user.</param>
        internal static void ShowInfo(string message)
        {
            MessageBox.Show(message,
                "Информация",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
