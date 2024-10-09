using DataTier.Models;
using System.Data.SQLite;

namespace DataTier.Database
{
    /*  class used to perform operations
     *  on the Transactions table;
     *  shouldn't have a Delete or Update method;
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
                        command.Parameters.AddWithValue("@Amount", transaction.Amount);
                        command.Parameters.AddWithValue("@AccountID", transaction.AccountID);

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
                Console.WriteLine("Error (TransactionOps.Insert): " + oof.Message);
                return false;
            }

            return false;
        }

        public static List<Transaction> GetAll()
        {
            // initalize list of transactions
            List<Transaction> transactionList = new List<Transaction>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // query to retrieve all records from Transactions table
                        command.CommandText = "SELECT * FROM Transactions";

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // create new transaction with read values
                                Transaction transaction = new Transaction(
                                    Convert.ToDouble(reader["Amount"]),
                                    Convert.ToInt32(reader["AccountID"])
                                );

                                // add transaction to transactionList
                                transactionList.Add(transaction);
                            }
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
            }

            // return list containing all transactions
            return transactionList;
        }

        public static List<Transaction> GetTransactionsByID(int accountID)
        {
            // initalize list of transactions
            List<Transaction> transactionList = new List<Transaction>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // query to retrieve transactions based on primary key
                        command.CommandText = "SELECT * FROM Transactions WHERE AccountID = @AccountID";
                        command.Parameters.AddWithValue("@AccountID", accountID);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // create new transaction with read values
                                Transaction transaction = new Transaction(
                                    Convert.ToDouble(reader["Amount"]),
                                    Convert.ToInt32(reader["AccountID"])
                                );

                                // add transaction to transactionList
                                transactionList.Add(transaction);
                            }
                        }
                    }
                }
            }
            catch (Exception oof)
            {
                Console.WriteLine("Error: " + oof.Message);
            }

            return transactionList;
        }
    }
}
