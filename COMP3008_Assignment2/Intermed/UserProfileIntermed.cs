using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermed
{
    public class UserProfileIntermed
    {
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private long phone;
        private string password;
        private int accountID;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public long Phone { get { return phone; } set { phone = value; } }
        public string Password { get { return password; } set { password = value; } }
        public int AccountID { get { return accountID; } set { accountID = value; } }

        public override string ToString()
        {
            return "First Name: " + FirstName + "\nLast Name: " + LastName + "\nEmail: " + Email + "\nAddress: " + Address + "\nPhone: " + Phone + "\nPassword: " + Password + "\nAccountID: " + AccountID;
        }

    }
}
