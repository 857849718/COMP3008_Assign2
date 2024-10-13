namespace DataTier.Models
{
    public class Transaction
    {
        // class fields
        private double amount; // deposited/withdrawn amount
        private int accountID;
        private string description;
        private string time;

        // constructor
        public Transaction(double amount, int accountID, string description, string time)
        {
            this.amount = amount;
            this.accountID = accountID;
            this.description = description;
            this.time = time;
        }

        // accessors/mutators
        public double Amount { get { return amount; } set { amount = value; } }
        public int AccountID { get { return accountID; } set { accountID = value; } }

        public string Description { get { return description; } set { description = value; } }

        public string Time { get { return time; } set { time = value; } }
    }
}
