using SportsPro.Models;

namespace SportsPro.ViewModels
{
    public class IncidentAddEditViewModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Technician> Technicians { get; set; } = new List<Technician>();

        public Incident Incident { get; set; } = new Incident();

        // "Add" or "Edit"
        public string Action { get; set; } = "Add";
    }
}
