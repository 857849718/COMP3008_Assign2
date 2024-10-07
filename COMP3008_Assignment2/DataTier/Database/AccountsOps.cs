using DataTier.Models;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Security.Principal;

namespace DataTier.Database
{
    /*  class used to perform operations
     *  on the Accounts table;
     */
    public class AccountsOps
    {
        private static string connectString = "Data Source=bank.db;Version=3;";
        public static bool Insert(Account account)
        {
            try
            {
                // create new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // command parameters
                        command.CommandText = @"
                        INSERT INTO Accounts (Balance, FirstName, Lastname)
                        VALUES (@Balance, @FirstName, @Lastname)";

                        // pararmeters inserted
                        command.Parameters.AddWithValue("@Balance", account.balance);
                        command.Parameters.AddWithValue("@FirstName", account.firstName);
                        command.Parameters.AddWithValue("@LastName", account.lastName);

                        int rowsInserted = command.ExecuteNonQuery();
                        
                        // ensure record has been inserted
                        if (rowsInserted > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
                return false;
            }

            return false;
        }

        public static bool Delete(int accountID)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // retrieve account based on primary key
                        command.CommandText = $"DELETE FROM Accounts WHERE AccountID = @AccountID";
                        command.Parameters.AddWithValue("@AccountID", accountID);

                        int rowsDeleted = command.ExecuteNonQuery();

                        // ensure record has been deleted
                        if (rowsDeleted > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
                return false;
            }

            return false;
        }

        public static bool Update(Account account)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // locate account based on primary key
                        command.CommandText = $"UPDATE Accounts SET Balance = @Balance, FirstName = @FirstName, LastName = @LastName WHERE AccountID = @AccountID";

                        command.Parameters.AddWithValue("@Balance", account.balance);
                        command.Parameters.AddWithValue("@FirstName", account.firstName);
                        command.Parameters.AddWithValue("@LastName", account.lastName);
                        command.Parameters.AddWithValue("@AccountID", account.accountID);

                        int rowsUpdated = command.ExecuteNonQuery();

                        // ensure record has been updated
                        if (rowsUpdated > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
                return false;
            }

            return false;
        }

        public static List<Account> GetAll()
        {
            // initalize list of accounts
            List<Account> accountList = new List<Account>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // query to retrieve all records from Accounts table
                        command.CommandText = "SELECT * FROM Accounts";

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // create new account with read values
                                Account account = new Account(
                                    Convert.ToDouble(reader["Balance"]),
                                    reader["FirstName"].ToString(),
                                    reader["LastName"].ToString()
                                );

                                // add account to accountList
                                accountList.Add(account);
                            }
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
            }

            // return list containing all accounts
            return accountList;
        }

        public static Account GetAccountByID(int accountID)
        {
            // initialize account
            Account account = null;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // query to retrieve account based on primary key
                        command.CommandText = "SELECT * FROM Accounts WHERE AccountID = @AccountID";
                        command.Parameters.AddWithValue("@AccountID", accountID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                account = new Account(
                                    Convert.ToDouble(reader["Balance"]),
                                    reader["FirstName"].ToString(),
                                    reader["LastName"].ToString()
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
            }

            return account;
        }
    }
}
