using DataTier.Models;
using System.Data.SQLite;

namespace DataTier.Database
{
    /*  class used to perform operations
     *  on the Transactions table;
     */
    public class TransactionsOps
    {
        private static string connectString = "Data Source=bank.db;Version=3;";

        public static bool Insert(Transaction transaction)
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
                        INSERT INTO Transactions (Amount, AccountID)
                        VALUES (@Amount, @AccountID)";

                        // pararmeters inserted
                        command.Parameters.AddWithValue("@Transactions", transaction.Amount);
                        command.Parameters.AddWithValue("@FirstName", account.FirstName);

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
    }
}
