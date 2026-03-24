using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportsPro.Models.ViewModels
{
    public class IncidentAddEditViewModel
    {
        public Incident Incident { get; set; } = new();

        // Spec says: store lists
        public List<Customer> Customers { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<Technician> Technicians { get; set; } = new();

        // Helpful for view
        public string Action { get; set; } = "Add"; // "Add" or "Edit"

        // Tag-helper-friendly dropdowns (computed from lists)
        public IEnumerable<SelectListItem> CustomerItems =>
            Customers.ConvertAll(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = $"{c.FirstName} {c.LastName}"
            });

        public IEnumerable<SelectListItem> ProductItems =>
            Products.ConvertAll(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.Name
            });

        public IEnumerable<SelectListItem> TechnicianItems =>
            Technicians.ConvertAll(t => new SelectListItem
            {
                Value = t.TechnicianID.ToString(),
                Text = t.Name
            });
    }
}