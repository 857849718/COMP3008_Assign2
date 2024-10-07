namespace DataTier.Models
{
    public class Account
    {
        // class fields
        public int accountID { get; set; }
        public double balance { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        // constructor
        public Account(double balance, string firstName, string lastName)
        {
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
