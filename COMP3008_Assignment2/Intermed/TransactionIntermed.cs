using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermed
{
    public class TransactionIntermed
    {
        private double amount;
        private int accountID;
        private string description;
        private string time;

        public double Amount { get { return amount; } set { amount = value; } }
        public int AccountID { get { return accountID; } set { accountID = value; } }

        public string Description { get { return description; } set { description = value; } }

        public string Time { get { return time; } set { time = value; } }
    }
}
