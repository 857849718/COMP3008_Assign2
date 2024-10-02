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
                        
                    }
                }
            }
        }
    }
}
