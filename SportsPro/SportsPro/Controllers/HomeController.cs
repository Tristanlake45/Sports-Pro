using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        [HttpGet("/")]
        public IActionResult Index() => View();

        // supports: /about/  AND  /home/about/
        [HttpGet("/about/")]
        [HttpGet("about/")]
        public IActionResult About() => View();
    }
}