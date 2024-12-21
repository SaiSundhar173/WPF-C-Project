using System.Windows;
using WpfApp1;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DatabaseHelper.initializeDatabase();
            InitializeComponent();
        }

        private void GreetButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the username from the TextBox
            string username = UsernameTextBox.Text;
            DatabaseHelper.insertName(username);
            // Check if the username is empty
            if (string.IsNullOrWhiteSpace(username))
            {
                GreetingLabel.Content = "Please enter a valid username!";
            }
            else
            {
                // Display the greeting
                GreetingLabel.Content = $"Hello, {username}!";
            }
        }
    }
}
