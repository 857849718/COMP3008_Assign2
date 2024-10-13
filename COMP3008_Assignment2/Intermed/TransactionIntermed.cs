using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermed
{
    internal class TransactionIntermed
    {
        private double amount;
        private int accountID;

        public double Amount { get { return amount; } set { amount = value; } }
        public int AccountID { get { return accountID; } set { accountID = value; } }

    }
}
