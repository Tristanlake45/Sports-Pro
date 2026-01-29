using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product code is required.")]
        public string ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yearly price is required.")]
        [Range(0.01, 999999, ErrorMessage = "Yearly price must be greater than 0.")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal YearlyPrice { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
    }
}

