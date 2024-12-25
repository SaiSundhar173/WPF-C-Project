using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp1;

namespace FinanceTracker
{
    public partial class MainWindow : Window
    {
        private List<Expense> _expenses;

        public MainWindow()
        {
            DatabaseHelper.initializeDatabase();
            InitializeComponent();
            _expenses = DatabaseHelper.LoadExpenses();
            LoadRecentTransactions();
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(ExpenseDate.Text, out var date) &&
                decimal.TryParse(ExpenseAmount.Text, out var amount))
            {
                var expense = new Expense
                {
                    Date = date,
                    Section = ExpenseSection.Text,
                    Description = ExpenseDescription.Text,
                    Amount = amount
                };

                DatabaseHelper.AddExpense(expense);
                _expenses.Add(expense);
                LoadRecentTransactions();

                ExpenseDate.SelectedDate = null;
                ExpenseSection.Clear();
                ExpenseDescription.Clear();
                ExpenseAmount.Clear();

                MessageBox.Show("Expense added successfully.");
            }
            else
            {
                MessageBox.Show("Please enter valid data.");
            }
        }

        private void LoadRecentTransactions()
        {
            RecentTransactionsList.ItemsSource = _expenses
                .OrderByDescending(e => e.Date)
                .Take(10)
                .ToList();
        }
    }

    public class Expense
    {
        public DateTime Date { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
