namespace DataTier.Models
{
    public class Transaction
    {
        // class fields
        private double amount; // deposited/withdrawn amount
        private int accountID;

        // constructor
        public Transaction(double amount, int accountID)
        {
            this.amount = amount;
            this.accountID = accountID;
        }

        // accessors/mutators
        public double Amount { get; set; }
        public int AccountID { get; set; }
     }
}
