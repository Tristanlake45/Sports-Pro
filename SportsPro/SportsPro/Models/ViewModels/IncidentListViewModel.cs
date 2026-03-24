using System.Collections.Generic;
using SportsPro.Models;

namespace SportsPro.Models.ViewModels
{
    public class IncidentListViewModel
    {
        public List<Incident> Incidents { get; set; } = new();
        public string Filter { get; set; } = "all"; // "all", "unassigned", "open" (used later)
    }
}