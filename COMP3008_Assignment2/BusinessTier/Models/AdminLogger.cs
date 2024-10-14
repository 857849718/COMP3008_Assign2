namespace BusinessTier.Models
{
    public class AdminLogger
    {
        private int logNumber = 0;

        // class to log admin activities
        public static void Log(string action)
        {
            FileStream fileStream = new FileStream("admin_log.txt", FileMode.Append, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string currentTime = DateTime.Now.ToString("h:mm:ss tt");
                string time = " TIME -> " + currentTime;

                string logNo = "LOG NUMBER -> " + logNumber;

                string newLongString = logNo + action + time;

                writer.WriteLine(newLongString);
                logNumber++;
            }
        }
    }
}
