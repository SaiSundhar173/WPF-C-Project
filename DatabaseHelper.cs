using System;
using System.Collections.Generic;
using System.Data.SQLite;
using FinanceTracker;

namespace WpfApp1
{
    public static class DatabaseHelper
    {
        private const string databasePath = @"..\..\..\db\Expense.db";
        private const string ConnectionString = "Data Source=" + databasePath + ";Version=3;";

        public static void initializeDatabase()
        {
            // Check if the database already exists
            if (!System.IO.File.Exists(databasePath))
            {
                // Create the database file
                SQLiteConnection.CreateFile(databasePath);
                Console.WriteLine("Database created successfully.");
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                var createTableQuery = "CREATE TABLE Expenses (" +
                                       "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                       "Date TEXT, " +
                                       "Section TEXT, " +
                                       "Description TEXT, " +
                                       "Amount REAL)";
                using var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void AddExpense(Expense expense)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            var query = "INSERT INTO Expenses (Date, Section, Description, Amount) VALUES (@Date, @Section, @Description, @Amount)";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Date", expense.Date);
            command.Parameters.AddWithValue("@Section", expense.Section);
            command.Parameters.AddWithValue("@Description", expense.Description);
            command.Parameters.AddWithValue("@Amount", expense.Amount);
            command.ExecuteNonQuery();
        }

        public static List<Expense> LoadExpenses()
        {
            var expenses = new List<FinanceTracker.Expense>();
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            var query = "SELECT Date, Section, Description, Amount FROM Expenses";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                expenses.Add(new Expense
                {
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    Section = reader["Section"].ToString(),
                    Description = reader["Description"].ToString(),
                    Amount = decimal.Parse(reader["Amount"].ToString())
                });
            }

            return expenses;
        }
    }
}
