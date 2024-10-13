namespace DataTier.Models
{
    public class UserProfile
    {
        // class fields
        /*
         * Notes:
         *      address could maybe be created out of
         *      multiple attributes, e.g. streetNumber,
         *      street, suburb, city;
         *      need to add images for profile pictures
         */
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private long phone;
        private string password;
        private int accountID;

        // constructor
        public UserProfile(string firstName, string lastName, string email, string address, long phone, string password, int accountID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.phone = phone;
            this.password = password;
            this.accountID = accountID;
        }
        
        // accessors/mutators
        public string FirstName { get { return firstName; } set { firstName = value;  } }
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
