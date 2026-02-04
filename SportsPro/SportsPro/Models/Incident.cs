using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Incident
    {
        public int IncidentID { get; set; }

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please select a product.")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please select a technician.")]
        public int TechnicianID { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public DateTime DateOpened { get; set; }

        // optional per rubric
        public DateTime? DateClosed { get; set; }

        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
        public Technician? Technician { get; set; }
    }
}