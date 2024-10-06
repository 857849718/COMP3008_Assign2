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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }
        public int AccountID { get; set; }

    }
}
