using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    [Route("incident")]
    public class IncidentController : Controller
    {
        private readonly SportsProContext _context;

        public IncidentController(SportsProContext context)
        {
            _context = context;
        }

        // GET: /incidents
        [HttpGet("/incidents")]
        [HttpGet("list/")]
        public IActionResult List()
        {
            var viewModel = new IncidentListViewModel
            {
                Incidents = _context.Incidents
                    .Include(i => i.Customer)
                    .Include(i => i.Product)
                    .Include(i => i.Technician)
                    .OrderByDescending(i => i.DateOpened)
                    .ToList(),
                Filter = "all"
            };

            return View(viewModel);
        }

        // GET: /incident/add/
        [HttpGet("add/")]
        public IActionResult Add()
        {
            var viewModel = BuildAddEditViewModel(new Incident { DateOpened = DateTime.Today }, "Add");
            return View("AddEdit", viewModel);
        }

        // POST: /incident/add/
        [HttpPost("add/")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEdit", BuildAddEditViewModel(incident, "Add"));
            }

            _context.Incidents.Add(incident);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /incident/edit/4/
        [HttpGet("edit/{id:int}/")]
        public IActionResult Edit(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident == null) return NotFound();

            return View("AddEdit", BuildAddEditViewModel(incident, "Edit"));
        }

        // POST: /incident/edit/4/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Incident incident)
        {
            if (id != incident.IncidentID) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View("AddEdit", BuildAddEditViewModel(incident, "Edit"));
            }

            _context.Incidents.Update(incident);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /incident/delete/4/
        [HttpGet("delete/{id:int}/")]
        public IActionResult Delete(int id)
        {
            var incident = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefault(i => i.IncidentID == id);

            if (incident == null) return NotFound();

            return View(incident);
        }

        // POST: /incident/delete/4/
        [HttpPost("delete/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident == null) return RedirectToAction(nameof(List));

            _context.Incidents.Remove(incident);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        private IncidentAddEditViewModel BuildAddEditViewModel(Incident incident, string action)
        {
            return new IncidentAddEditViewModel
            {
                Incident = incident,
                Action = action,
                Customers = _context.Customers
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ToList(),
                Products = _context.Products
                    .OrderBy(p => p.ProductCode)
                    .ToList(),
                Technicians = _context.Technicians
                    .Where(t => t.TechnicianID != -1)
                    .OrderBy(t => t.Name)
                    .ToList()
            };
        }
    }
}