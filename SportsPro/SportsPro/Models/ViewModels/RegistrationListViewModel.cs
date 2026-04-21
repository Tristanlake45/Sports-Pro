using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.Models;

namespace SportsPro.ViewModels
{
    public class RegistrationListViewModel
    {
        public Customer? CurrentCustomer { get; set; }
        public List<Registration> Registrations { get; set; } = new();
        public List<Product> AvailableProducts { get; set; } = new();

        public IEnumerable<SelectListItem> ProductItems =>
            AvailableProducts.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            });
    }
}