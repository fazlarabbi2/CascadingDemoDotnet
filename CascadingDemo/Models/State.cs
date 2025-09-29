namespace CascadingDemo.Models
{
    // Represents states which are linked to a Country via CountryId.
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        // Navigation properties
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}