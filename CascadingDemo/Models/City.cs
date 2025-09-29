namespace CascadingDemo.Models
{
    //Represents cities which are linked to a State via StateId.
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        // Navigation property
        public State State { get; set; }
    }
}