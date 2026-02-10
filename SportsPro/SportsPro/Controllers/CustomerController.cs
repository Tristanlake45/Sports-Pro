using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly SportsProContext _context;

        public CustomerController(SportsProContext context)
        {
            _context = context;
        }

        // GET: /customers
        [HttpGet("/customers")]
        [HttpGet("list/")]
        public IActionResult List()
        {
            var customers = _context.Customers
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToList();

            return View(customers);
        }

        // GET: /customer/add/
        [HttpGet("add/")]
        public IActionResult Add()
        {
            LoadCountries();
            return View("AddEdit", new Customer());
        }

        // POST: /customer/add/
        [HttpPost("add/")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Customer customer)
        {
            // IMPORTANT: reload dropdowns when invalid
            if (!ModelState.IsValid)
            {
                LoadCountries();
                return View("AddEdit", customer);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /customer/edit/1002/
        [HttpGet("edit/{id:int}/")]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            LoadCountries(customer.CountryID);
            return View("AddEdit", customer);
        }

        // POST: /customer/edit/1002/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerID) return BadRequest();

            if (!ModelState.IsValid)
            {
                LoadCountries(customer.CountryID);
                return View("AddEdit", customer);
            }

            Console.WriteLine($"CountryID posted: '{customer.CountryID}'");
            _context.Customers.Update(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        // GET: /customer/delete/1002/
        [HttpGet("delete/{id:int}/")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: /customer/delete/1002/
        [HttpPost("delete/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return RedirectToAction(nameof(List));

            Console.WriteLine($"CountryID posted: '{customer.CountryID}'");
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        private void LoadCountries(string? selectedCountryId = null)
        {
            var countries = _context.Countries
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Countries = new SelectList(countries, "CountryID", "Name", selectedCountryId);
        }
    }
}