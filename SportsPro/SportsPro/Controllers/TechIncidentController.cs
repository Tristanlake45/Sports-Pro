using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Route("techincident")]
    public class TechIncidentController : Controller
    {
        private readonly SportsProContext _context;
        private const string TechnicianKey = "TechnicianID";

        public TechIncidentController(SportsProContext context)
        {
            _context = context;
        }

        // GET: /techincident/
        [HttpGet("")]
        public IActionResult Index()
        {
            LoadTechnicians();
            return View();
        }

        // POST: /techincident/
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int technicianId)
        {
            if (technicianId == 0)
            {
                ModelState.AddModelError("TechnicianID", "You must select a technician.");
                LoadTechnicians();
                return View();
            }

            HttpContext.Session.SetInt32(TechnicianKey, technicianId);

            // Supports the screenshot style URL: /techincident/list/14/
            return RedirectToAction(nameof(List), new { id = technicianId });
        }

        // GET: /techincident/list/14/
        // GET: /techincident/list/
        [HttpGet("list/{id?}/")]
        public IActionResult List(int? id)
        {
            // If an id is passed in the URL, store it in session
            if (id.HasValue && id.Value > 0)
            {
                HttpContext.Session.SetInt32(TechnicianKey, id.Value);
            }

            int? technicianId = HttpContext.Session.GetInt32(TechnicianKey);

            if (!technicianId.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var technician = _context.Technicians
                .FirstOrDefault(t => t.TechnicianID == technicianId.Value);

            if (technician == null)
            {
                HttpContext.Session.Remove(TechnicianKey);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Technician = technician;

            var incidents = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .Where(i => i.TechnicianID == technicianId.Value && i.DateClosed == null)
                .OrderBy(i => i.DateOpened)
                .ToList();

            return View(incidents);
        }

        // GET: /techincident/edit/2/
        [HttpGet("edit/{id:int}/")]
        public IActionResult Edit(int id)
        {
            int? technicianId = HttpContext.Session.GetInt32(TechnicianKey);

            if (!technicianId.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var incident = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefault(i => i.IncidentID == id);

            if (incident == null)
            {
                return NotFound();
            }

            // Prevent editing incidents not assigned to current technician
            if (incident.TechnicianID != technicianId.Value)
            {
                return RedirectToAction(nameof(List));
            }

            return View(incident);
        }

        // POST: /techincident/edit/2/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Incident model)
        {
            int? technicianId = HttpContext.Session.GetInt32(TechnicianKey);

            if (!technicianId.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var incident = _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefault(i => i.IncidentID == id);

            if (incident == null)
            {
                return NotFound();
            }

            if (incident.TechnicianID != technicianId.Value)
            {
                return RedirectToAction(nameof(List));
            }

            // Only update the fields technicians are allowed to edit
            incident.Description = model.Description;
            incident.DateClosed = model.DateClosed;

            // Server-side validation for required description
            if (string.IsNullOrWhiteSpace(incident.Description))
            {
                ModelState.AddModelError("Description", "The Description field is required.");
            }

            if (!ModelState.IsValid)
            {
                // Re-load related data so the view can still display names
                return View(incident);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /techincident/switch/
        [HttpGet("switch/")]
        public IActionResult Switch()
        {
            HttpContext.Session.Remove(TechnicianKey);
            return RedirectToAction(nameof(Index));
        }

        private void LoadTechnicians()
        {
            var technicians = _context.Technicians
                .Where(t => t.TechnicianID != -1)
                .OrderBy(t => t.Name)
                .ToList();

            ViewBag.Technicians = new SelectList(technicians, "TechnicianID", "Name");
        }
    }
}