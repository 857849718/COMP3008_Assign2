namespace BusinessTier.Models
{
    public class AdminLogger
    {
        private static int logNumber = 0;

        // class to log admin activities
        public static void Log(string action, Boolean success)
        {
            FileStream fileStream = new FileStream("admin_log.txt", FileMode.Append, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string currentTime = DateTime.Now.ToString("h:mm:ss tt");
                string time = " TIME: " + currentTime;

                string logNo = "LOG NUMBER: " + logNumber+" |";
                action = "ACTION: " + action + " |";

                string result = "";
                if (success)
                {
                    result = "RESULT: SUCCESSFUL | ";
                }
                else
                {
                    result = "RESULT: FAILED | ";
                }

                string newLongString = logNo + action + result + time;

                writer.WriteLine(newLongString);
                logNumber++;
            }
        }
    }
}
