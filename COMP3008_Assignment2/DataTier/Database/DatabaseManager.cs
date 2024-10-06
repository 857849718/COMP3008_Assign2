using System.Data.SQLite;
using System.Linq.Expressions;

namespace DataTier.Database
{
    public class DatabaseManager
    {
        private static string connectString = "Data Source=bank.db;Version=3;";

        public static bool CreateTables()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        /*  Accounts table;
                         *  AccountID will automatically increment
                         */
                        command.CommandText = @"CREATE TABLE Accounts (
                            AccountID INTEGER AUTOINCREMENT, 
                            Balance REAL,
                            FirstName TEXT,
                            LastName TEXT,
                            PRIMARY KEY (AccountID)
                        )";

                        command.ExecuteNonQuery();

                        /*  Transactions table;
                         *  TransactionID will automatically increment;
                         *  References AccountID as a foreign key
                         */
                        command.CommandText = @"CREATE TABLE Transactions (
                            TransactionID INTEGER AUTOINCREMENT,
                            Amount REAL,
                            AccountID INTEGER,
                            PRIMARY KEY (TransactionID),
                            FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
                        )";

                        command.ExecuteNonQuery();

                        /*  Profiles table;
                         *  Uses Email as a primary key (no two emails should be the same);
                         *  References AccountID as a foreign key
                         */
                        command.CommandText = @"CREATE TABLE Profiles (
                            FirstName TEXT,
                            LastName TEXT,
                            Email TEXT,
                            Address TEXT,
                            Phone INTEGER,
                            Password TEXT,
                            AccountID INTEGER,
                            PRIMARY KEY (Email),
                            FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
                        )";

                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Tables created successfully!");
                    return true;
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine(oof.Message);
            }
            return false;
        }
    }
}
