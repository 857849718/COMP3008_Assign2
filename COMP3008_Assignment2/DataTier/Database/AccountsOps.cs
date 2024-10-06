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
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        INSERT INTO Accounts (Balance, FirstName, Lastname)
                        VALUES (@Balance, @FirstName, @Lastname)";

                        command.Parameters.AddWithValue("@Balance", account.Balance);
                        command.Parameters.AddWithValue("@FirstName", account.FirstName);
                        command.Parameters.AddWithValue("@LastName", account.LastName);

                        int rowsInserted = command.ExecuteNonQuery();

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
                        command.CommandText = $"DELETE FROM Accounts WHERE AccountID = @AccountID";
                        command.Parameters.AddWithValue("@AccountID", accountID);

                        int rowsDeleted = command.ExecuteNonQuery();

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
                        command.CommandText = $"UPDATE Accounts SET Balance = @Balance, FirstName = @FirstName, LastName = @LastName WHERE AccountID = @AccountID";

                        command.Parameters.AddWithValue("@Balance", account.Balance);
                        command.Parameters.AddWithValue("@FirstName", account.FirstName);
                        command.Parameters.AddWithValue("@LastName", account.LastName);
                        command.Parameters.AddWithValue("@AccountID", account.AccountID);

                        int rowsUpdated = command.ExecuteNonQuery();

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
            List<Account> accountList = new List<Account>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Accounts";

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Account account = new Account();
                                account.AccountID = Convert.ToInt32(reader["AccountID"]);
                                account.Balance = Convert.ToDouble(reader["Balance"]);
                                account.FirstName = reader["FirstName"].ToString();
                                account.LastName = reader["LastName"].ToString();
                                
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

            return accountList;
        }
    }
}
