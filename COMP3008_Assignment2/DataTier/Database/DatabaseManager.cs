using DataTier.Models;
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
                        command.CommandText = @"CREATE TABLE IF NOT EXISTS Accounts (
                            AccountID INTEGER PRIMARY KEY AUTOINCREMENT, 
                            Balance REAL,
                            FirstName TEXT,
                            LastName TEXT
                        )";

                        command.ExecuteNonQuery();

                        /*  Transactions table;
                         *  TransactionID will automatically increment;
                         *  References AccountID as a foreign key
                         */
                        command.CommandText = @"CREATE TABLE IF NOT EXISTS Transactions (
                            TransactionID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Amount REAL,
                            AccountID INTEGER,
                            FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
                        )";

                        command.ExecuteNonQuery();

                        /*  Profiles table;
                         *  Uses Email as a primary key (no two emails should be the same);
                         *  References AccountID as a foreign key
                         */
                        command.CommandText = @"CREATE TABLE IF NOT EXISTS Profiles (
                            FirstName TEXT,
                            LastName TEXT,
                            Email TEXT PRIMARY KEY,
                            Address TEXT,
                            Phone INTEGER,
                            Password TEXT,
                            AccountID INTEGER,
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

        public static void InitializeAccounts()
        {
            if (CreateTables())
            {
                Account account = new Account
                (
                    balance: 5150,
                    firstName: "Ben",
                    lastName: "Tom"
                );
                AccountsOps.Insert(account);

                Console.WriteLine("Balance for account: " + account.Balance);

                Account acc = new Account(null, null, null);
            }
        }
    }
}
