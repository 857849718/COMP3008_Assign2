using System.Data.SQLite;

namespace DataTier.Database
{
    public class DatabaseManager
    {
        private static string connectString = "Data Source=bank.db;Version=3;";

        public static bool CreateTables()
        {
            try
            {
                using(SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"CREATE TABLE Accounts (
                            AccountID INTEGER AUTOINCREMENT,
                            Balance REAL,
                            FirstName TEXT,
                            LastName TEXT,
                            PRIMARY KEY (AccountID)
                        )";

                        command.ExecuteNonQuery();

                        command.CommandText = @"CREATE TABLE Transactions (
                            TransactionID INTEGER AUTOINCREMENT,
                            DepositAmount REAL,
                            WithdrawnAmount REAL,
                            AccountID INTEGER,
                            PRIMARY KEY (TransactionID),
                            FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
                        )";

                        command.ExecuteNonQuery();

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
                }
            }
        }
    }
}
