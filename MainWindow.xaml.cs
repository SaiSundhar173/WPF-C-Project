using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GreetButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the username from the TextBox
            string username = UsernameTextBox.Text;

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
