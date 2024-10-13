using DataTier.Database;
using System.Security.Cryptography;

namespace DataTier.Models.DataSeeding
{
    public class RandomGenerator
    {
        private static List<String> firstNames = NamesList.FirstNames();
        private static List<String> lastNames = NamesList.LastNames(); 
        private static List<String> emails = InformationList.Emails();
        private static List<String> passwords = InformationList.Passwords();
        private static List<String> addresses = InformationList.Addresses();
        private static List<String> descriptions = InformationList.Descriptions();

        static Random rand = new Random();
        static int index;

        public static void Generate()
        {
            for (int i = 0; i < 8; i++)
            {
                int balance = getBalance();
                String firstName = getFirstName();
                String lastName = getLastName();
                String email = emails[i];
                String password = passwords[i];
                String address = addresses[i];
                int phone = getPhone();

                Account account = new Account(balance, firstName, lastName);
                AccountsOps.Insert(account);
                int accountID = AccountsOps.GetAccountID();

                UserProfile profile = new UserProfile(firstName, lastName, email, address, phone, password, accountID);
                ProfilesOps.Insert(profile);
            }

            for (int i = 0; i < 5; i++)
            {
                int accountID = getID();
                int amount = getAmount();
                String description = descriptions[i];

                Transaction transaction = new Transaction(amount, accountID, description, DateTime.Now.ToString());
                TransactionsOps.Insert(transaction);
            }
        }

        public static String getFirstName()
        {
            index = rand.Next(0, 19);
            return firstNames[index];
        }

        public static String getLastName() 
        {
            index = rand.Next(0, 19);
            return lastNames[index];
        }

        public static int getBalance()
        {
            return rand.Next(-100000, 100000);
        }

        public static int getAmount()
        {
            return rand.Next(-10000, 10000);
        }

        public static int getPhone()
        {
            return rand.Next(00000000, 99999999);
        }

        public static int getID()
        {
            return rand.Next(1, 8);
        }
    }
}
