namespace DataTier.Models
{
    public class Transaction
    {
        // class fields
        private double depositAmount;
        private double withdrawnAmount;
        private List<string> transactionHistory;

        // constructor
        public Transaction(double depositAmount, double withdrawnAmount, List<string> transactionHistory)
        {
            this.depositAmount = depositAmount;
            this.withdrawnAmount = withdrawnAmount;
            this.transactionHistory = transactionHistory;
        }

        // accessors/mutators
        public double DepositAmount { get; set; }
        public double WithdrawnAmount { get; set; }
        public List<string> TransactionHistory { get; set; }
     }
}
