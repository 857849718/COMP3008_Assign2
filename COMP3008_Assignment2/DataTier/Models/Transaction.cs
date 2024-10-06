namespace DataTier.Models
{
    public class Transaction
    {
        // class fields
        private double amount; // deposited/withdrawn amount

        // constructor
        public Transaction(double amount)
        {
            this.amount = amount;
        }

        // accessors/mutators
        public double Amount { get; set; }
     }
}
