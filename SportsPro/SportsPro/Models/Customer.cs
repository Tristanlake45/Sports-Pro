using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        public string State { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(20, MinimumLength = 1)]
        public string PostalCode { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        public string CountryID { get; set; } = "";

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = "";

        [RegularExpression(@"^$|^\(\d{3}\) \d{3}-\d{4}$",
            ErrorMessage = "Phone number must be in (999) 999-9999 format.")]
        public string? Phone { get; set; }

        public Country? Country { get; set; }

        public ICollection<Registration>? Registrations { get; set; }
    }
}