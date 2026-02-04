using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

        [Required]
        public string State { get; set; } = "";

        [Required]
        public string PostalCode { get; set; } = "";

        [Required(ErrorMessage = "Please select a country.")]
        public string CountryID { get; set; } = "";

        // optional per rubric
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // navigation is OK, just NOT [Required]
        public Country? Country { get; set; }
    }
}