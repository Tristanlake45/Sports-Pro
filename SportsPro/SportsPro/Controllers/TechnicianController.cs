using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Route("technician")]
    public class TechnicianController : Controller
    {
        private readonly SportsProContext _context;

        public TechnicianController(SportsProContext context)
        {
            _context = context;
        }

        // GET: /technician/list/
        [HttpGet("list/")]
        public IActionResult List()
        {
            var techs = _context.Technicians
                .Where(t => t.TechnicianID != -1)
                .OrderBy(t => t.Name)
                .ToList();

            return View(techs);
        }

        // GET: /technician/add/
        [HttpGet("add/")]
        public IActionResult Add()
        {
            return View("AddEdit", new Technician());
        }

        // POST: /technician/add/
        [HttpPost("add/")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Technician technician)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEdit", technician);
            }

            _context.Technicians.Add(technician);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /technician/edit/11/
        [HttpGet("edit/{id:int}/")]
        public IActionResult Edit(int id)
        {
            var tech = _context.Technicians.Find(id);
            if (tech == null)
            {
                return NotFound();
            }

            return View("AddEdit", tech);
        }

        // POST: /technician/edit/11/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Technician technician)
        {
            if (id != technician.TechnicianID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View("AddEdit", technician);
            }

            _context.Technicians.Update(technician);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /technician/delete/11/
        [HttpGet("delete/{id:int}/")]
        public IActionResult Delete(int id)
        {
            var tech = _context.Technicians.Find(id);
            if (tech == null)
            {
                return NotFound();
            }

            return View(tech);
        }

        // POST: /technician/delete/11/
        [HttpPost("delete/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tech = _context.Technicians.Find(id);
            if (tech == null)
            {
                return RedirectToAction(nameof(List));
            }

            _context.Technicians.Remove(tech);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /technician/cancel/
        [HttpGet("cancel/")]
        public IActionResult Cancel()
        {
            return RedirectToAction(nameof(List));
        }
    }
}
