namespace CascadingDemo.Models
{
    //Represents the master data for countries.
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        // Navigation property for States
        public ICollection<State> States { get; set; }
    }
}