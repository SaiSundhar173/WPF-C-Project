using System;
using System.Data.SQLite;

namespace WpfApp1
{
    public static class DatabaseHelper
    {
        public static void initializeDatabase()
        {
            string databasePath = @"..\..\..\db\Users.db"; // Path for the SQLite database file

            // Check if the database already exists
            if (!System.IO.File.Exists(databasePath))
            {
                // Create the database file
                SQLiteConnection.CreateFile(databasePath);
                Console.WriteLine("Database created successfully.");

                // Initialize the database schema
                using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
                {
                    connection.Open();

                    string createTableQuery = @"
                    CREATE TABLE Username (
                        name TEXT NOT NULL
                    )";

                    SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table 'Users' created successfully.");
                }
            }
            else
            {
                Console.WriteLine("Database already exists.");
            }
        }
        public static void insertName(string username)
        {
            string databasePath = @"..\..\..\db\Users.db";
            using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Username (name) VALUES (@name)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@name", username);
                command.ExecuteNonQuery();
                Console.WriteLine($"Username '{username}' inserted into the database.");
            }
        }
    }
}
