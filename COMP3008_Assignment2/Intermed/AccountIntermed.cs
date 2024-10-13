using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermed
{
    public class AccountIntermed
    {
        private int accountID;
        private double balance;
        private string firstName;
        private string lastName;
        public int AccountID { get { return accountID; } set { accountID = value;  } }
        public double Balance { get { return balance; } set { balance = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
    }
}
