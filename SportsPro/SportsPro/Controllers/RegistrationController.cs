using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    [Route("registration")]
    public class RegistrationController : Controller
    {
        private readonly SportsProContext _context;
        private const string CustomerKey = "CustomerID";

        public RegistrationController(SportsProContext context)
        {
            _context = context;
        }

        [HttpGet("/registrations")]
        [HttpGet("getcustomer/")]
        public IActionResult GetCustomer()
        {
            LoadCustomers();
            return View("GetCustomer");
        }

        [HttpPost("/registrations")]
        [HttpPost("getcustomer/")]
        [ValidateAntiForgeryToken]
        public IActionResult GetCustomer(int customerId)
        {
            if (customerId == 0)
            {
                ModelState.AddModelError("CustomerID", "You must select a customer.");
                LoadCustomers();
                return View("GetCustomer");
            }

            HttpContext.Session.SetInt32(CustomerKey, customerId);
            return RedirectToAction(nameof(List));
        }

        [HttpGet("list/")]
        public IActionResult List()
        {
            int? customerId = HttpContext.Session.GetInt32(CustomerKey);
            if (!customerId.HasValue)
            {
                return RedirectToAction(nameof(GetCustomer));
            }

            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == customerId.Value);

            if (customer == null)
            {
                HttpContext.Session.Remove(CustomerKey);
                return RedirectToAction(nameof(GetCustomer));
            }

            var registrations = _context.Registrations
                .Include(r => r.Product)
                .Where(r => r.CustomerID == customerId.Value)
                .OrderBy(r => r.Product!.Name)
                .ToList();

            var registeredProductIds = registrations
                .Select(r => r.ProductID)
                .ToList();

            var availableProducts = _context.Products
                .Where(p => !registeredProductIds.Contains(p.ProductID))
                .OrderBy(p => p.Name)
                .ToList();

            var vm = new RegistrationListViewModel
            {
                CurrentCustomer = customer,
                Registrations = registrations,
                AvailableProducts = availableProducts
            };

            return View(vm);
        }

        [HttpPost("register/")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(int productId)
        {
            int? customerId = HttpContext.Session.GetInt32(CustomerKey);
            if (!customerId.HasValue)
            {
                return RedirectToAction(nameof(GetCustomer));
            }

            bool exists = _context.Registrations.Any(r =>
                r.CustomerID == customerId.Value && r.ProductID == productId);

            if (!exists && productId != 0)
            {
                _context.Registrations.Add(new Registration
                {
                    CustomerID = customerId.Value,
                    ProductID = productId
                });

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost("delete/{productId:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId)
        {
            int? customerId = HttpContext.Session.GetInt32(CustomerKey);
            if (!customerId.HasValue)
            {
                return RedirectToAction(nameof(GetCustomer));
            }

            var registration = _context.Registrations.FirstOrDefault(r =>
                r.CustomerID == customerId.Value && r.ProductID == productId);

            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(List));
        }

        private void LoadCustomers()
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

            ViewBag.Customers = new SelectList(customers, "CustomerID", "FullName");
        }
    }
}