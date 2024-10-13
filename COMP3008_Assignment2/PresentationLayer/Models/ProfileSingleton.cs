namespace PresentationLayer.Models
{
    public class ProfileSingleton
    {
        private ProfileSingleton() { }
        private string email;

        private static ProfileSingleton instance;

        public static ProfileSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new ProfileSingleton();
            }
            return instance;
        }

        public string Email { get { return email; } set { email = value; } }
    }
}
