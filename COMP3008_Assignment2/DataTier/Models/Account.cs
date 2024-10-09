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
        public Account(double balance, string firstName, string lastName)
        {
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // constructor with accountID (for reading)
        public Account(int accountID, double balance, string firstName, string lastName)
        {
            this.accountID = accountID;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // parameterless constructor
        public Account() { }

        // accessors/mutators
        public int AccountID { get { return accountID; } set { accountID = value;  } }
        public double Balance { get { return balance; } set { balance = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }

        public override string ToString()
        {
            string accountDetails = "Account ID: " + accountID +
                "\n Balance: " + balance +
                "\n First Name: " + firstName +
                "\n Last Name: " + lastName;

            return accountDetails;
        }
    }
}
