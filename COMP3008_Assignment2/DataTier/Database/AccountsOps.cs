using DataTier.Models;
using System.Data.SQLite;

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
            }

            return false;
        }
    }
}
