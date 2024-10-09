namespace DataTier.Models.DataSeeding
{
    public class RandomGenerator
    {
        private List<String> firstNames = FirstNameList.FirstNames();
        private List<String> lastNames = LastNameList.LastNames(); 
    }
}
