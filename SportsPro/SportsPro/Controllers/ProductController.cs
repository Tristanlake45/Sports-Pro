using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly SportsProContext _context;

        public ProductController(SportsProContext context)
        {
            _context = context;
        }

        // GET: /products
        [HttpGet("/products")]
        [HttpGet("list/")]
        public ViewResult List()
        {
            var products = _context.Products.OrderBy(p => p.ProductCode).ToList();
            return View(products);
        }

        // GET: /product/add/
        [HttpGet("add/")]
        public ViewResult Add()
        {
            var product = new Product
            {
                ReleaseDate = DateTime.Today
            };

            return View("AddEdit", product);
        }

        // POST: /product/add/
        [HttpPost("add/")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEdit", product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["Message"] = "Product was added successfully.";
            return RedirectToAction(nameof(List));
        }

        // GET: /product/edit/5/
        [HttpGet("edit/{id:int}/")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View("AddEdit", product);
        }

        // POST: /product/edit/5/
        [HttpPost("edit/{id:int}/")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View("AddEdit", product);
            }

            _context.Products.Update(product);
            _context.SaveChanges();

            TempData["Message"] = "Product was updated successfully.";
            return RedirectToAction(nameof(List));
        }

        // GET: /product/delete/5/
        [HttpGet("delete/{id:int}/")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: /product/delete/5/
        [HttpPost("delete/{id:int}/")]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Message"] = "Product was deleted successfully.";
            }

            return RedirectToAction(nameof(List));
        }

        // GET: /product/cancel/
        [HttpGet("cancel/")]
        public RedirectToActionResult Cancel()
        {
            return RedirectToAction(nameof(List));
        }
    }
}
