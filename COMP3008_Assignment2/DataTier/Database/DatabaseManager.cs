using System.Data.SQLite;

namespace DataTier.Database
{
    public class DatabaseManager
    {
        string connectString = "Data Source=bank.db;Version=3;";
        SQLiteConnection connection = new SQLiteConnection(connectString);
    }
}
