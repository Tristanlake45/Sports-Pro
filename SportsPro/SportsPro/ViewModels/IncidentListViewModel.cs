using SportsPro.Models;

namespace SportsPro.ViewModels
{
    public class IncidentListViewModel
    {
        public List<Incident> Incidents { get; set; } = new List<Incident>();

        // "all", "unassigned", or "open"
        public string Filter { get; set; } = "all";
    }
}
