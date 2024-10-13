namespace DataTier.Models.DataSeeding
{
    public class InformationList
    {
        public static List<String> Emails()
        {
            List<String> emails = new List<String>();

            emails.Add("blockchain33@gmail.com");
            emails.Add("remotecallb4ck@yahoo.com");
            emails.Add("script.injection@hotmail.com");
            emails.Add("datawebapi@outlook.com");
            emails.Add("businessti3r@yahoo.com");
            emails.Add("jscript480@gmail.com");
            emails.Add("async_user43@hotmail.com");
            emails.Add("cloud.serverVM@gmail.com");
            return emails;
        }

        public static List<String> Passwords()
        {
            List<String> passwords = new List<String>();

            passwords.Add("password123");
            passwords.Add("s3cret");
            passwords.Add("bob_alert");
            passwords.Add("hashed_password");
            passwords.Add("plswork");
            passwords.Add("dshfsdk");
            passwords.Add("name_of_first_pet");
            passwords.Add("bozo6655");
            return passwords;
        }

        public static List<String> Addresses()
        {
            List<String> addresses = new List<String>();

            addresses.Add("6 Rupert Avenue");
            addresses.Add("100 Dexashire Road");
            addresses.Add("36 duPont Street");
            addresses.Add("82 Elm Street");
            addresses.Add("403 Juniper Avenue");
            addresses.Add("2 Seventh Road");
            addresses.Add("1002 Washington Street");
            addresses.Add("9 Grovewood Road");

            return addresses;
        }

        public static List<String> Descriptions()
        {
            List<String> descriptions = new List<String>();

            descriptions.Add("Withdraw for new PC");
            descriptions.Add("Withdraw to pay plumber");
            descriptions.Add("Deposit weekly salary");
            descriptions.Add("Deposit; pay from plumbing client");
            descriptions.Add("Withdraw to pay friend back");

            return descriptions;
        }
    }
}
