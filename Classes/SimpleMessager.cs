using System.Windows;

namespace RetailRentingApp.Classes
{
    /// <summary>
    /// The simple message proxy with some useful methods.
    /// </summary>
    public class SimpleMessager
    {
        /// <summary>
        /// The simple proxy for the MessageBox class to simplify error statements.
        /// </summary>
        /// <param name="message">An error message for a user.</param>
        public static void ShowError(string message)
        {
            _ = MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// The simple proxy for the MessageBox class to simplify "if" statements.
        /// </summary>
        /// <param name="message">An asking message for a user.</param>
        /// <returns>A bool result of an user's choice.</returns>
        public static bool ShowQuestion(string message)
        {
            MessageBoxResult result = MessageBox.Show(message,
                "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Shows a message information to a user.
        /// </summary>
        /// <param name="message">An information message for a user.</param>
        internal static void ShowInfo(string message)
        {
            _ = MessageBox.Show(message,
                "Информация",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
