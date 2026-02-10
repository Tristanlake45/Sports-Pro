using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

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
            var incidents = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .OrderByDescending(i => i.DateOpened)
                .ToList();

            return View(incidents);
        }

        // GET: /incident/add/
        [HttpGet("add/")]
        public IActionResult Add()
        {
            LoadIncidentDropdowns();
            return View("AddEdit", new Incident
            {
                DateOpened = DateTime.Today
            });
        }

        // POST: /incident/add/
        [HttpPost("add/")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                LoadIncidentDropdowns(incident.CustomerID, incident.ProductID, incident.TechnicianID);
                return View("AddEdit", incident);
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

            LoadIncidentDropdowns(incident.CustomerID, incident.ProductID, incident.TechnicianID);
            return View("AddEdit", incident);
        }

        // POST: /incident/edit/4/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Incident incident)
        {
            if (id != incident.IncidentID) return BadRequest();

            if (!ModelState.IsValid)
            {
                LoadIncidentDropdowns(incident.CustomerID, incident.ProductID, incident.TechnicianID);
                return View("AddEdit", incident);
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

        private void LoadIncidentDropdowns(int? selectedCustomerId = null, int? selectedProductId = null, int? selectedTechnicianId = null)
        {
            var customers = _context.Customers
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .Select(c => new
                {
                    c.CustomerID,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            var products = _context.Products
                .OrderBy(p => p.ProductCode)
                .ToList();

            var technicians = _context.Technicians
                .Where(t => t.TechnicianID != -1) // keep consistent with earlier assignments
                .OrderBy(t => t.Name)
                .ToList();

            ViewBag.Customers = new SelectList(customers, "CustomerID", "FullName", selectedCustomerId);
            ViewBag.Products = new SelectList(products, "ProductID", "Name", selectedProductId);
            ViewBag.Technicians = new SelectList(technicians, "TechnicianID", "Name", selectedTechnicianId);
        }
    }
}