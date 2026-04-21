using SportsPro.Models;

namespace SportsPro.ViewModels
{
    public class RegistrationViewModel
    {
        public Customer CurrentCustomer { get; set; } = null!;
        public List<Registration> Registrations { get; set; } = new();
        public List<Product> AvailableProducts { get; set; } = new();
    }
}