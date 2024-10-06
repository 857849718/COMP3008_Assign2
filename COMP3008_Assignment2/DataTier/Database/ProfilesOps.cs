using DataTier.Models;
using System.Data.SQLite;
using System.Security.Principal;

namespace DataTier.Database
{
    /*  class used to perform operations
     *  on the Profiles table;
     */
    public class ProfilesOps
    {
        private static string connectString = "Data Source=bank.db;Version=3;";

        public static bool Insert(UserProfile profile)
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
                        INSERT INTO Profiles (FirstName, LastName, Email, Address, Phone, Password, AccountID)
                        VALUES (@FirstName, @LastName, @Email, @Address, @Phone, @Password, @AccountID)";

                        // pararmeters inserted
                        command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                        command.Parameters.AddWithValue("@LastName", profile.LastName);
                        command.Parameters.AddWithValue("@Email", profile.Email);
                        command.Parameters.AddWithValue("@Address", profile.Address);
                        command.Parameters.AddWithValue("@Phone", profile.Phone);
                        command.Parameters.AddWithValue("@Password", profile.Password);
                        command.Parameters.AddWithValue("@AccountID", profile.AccountID);

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

        public static bool Delete(string email)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // retrieve profile based on primary key
                        command.CommandText = $"DELETE FROM Profiles WHERE Email = @Email";
                        command.Parameters.AddWithValue("@Email", email);

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

        public static bool Update(UserProfile profile)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // locate profile based on primary key
                        command.CommandText = $"UPDATE Profiles SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone, Password = @Password WHERE Email = @Email";

                        command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                        command.Parameters.AddWithValue("@LastName", profile.LastName);
                        command.Parameters.AddWithValue("@Address", profile.Address);
                        command.Parameters.AddWithValue("@Phone", profile.Phone);
                        command.Parameters.AddWithValue("@Password", profile.Password);

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
    }
}
