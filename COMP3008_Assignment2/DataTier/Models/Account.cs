namespace DataTier.Models
{
    public class Account
    {
        // class fields
        private int accountID;
        private double balance;
        private string firstName;
        private string lastName;

        // constructor
        public Account (int accountID, double balance, string firstName, string lastName)
        {
            this.accountID = accountID;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // accessors/mutators
        public int AccountID { get; set; }
        public double Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
