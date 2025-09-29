using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CascadingDemo.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid Phone number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
        // Use a [Range] to ensure a valid selection (nonzero)
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Country.")]
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required.")]
        public int? CountryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid State.")]
        [Display(Name = "State")]
        [Required(ErrorMessage = "Stat is required.")]
        public int? StateId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid City.")]
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        public int? CityId { get; set; }
        // Dropdown list for Countries; States and Cities will be loaded via AJAX
        public IEnumerable<SelectListItem>? Countries { get; set; }
    }
}
