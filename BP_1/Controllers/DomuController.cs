using BP_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BP_1.Controllers
{
    public class DomuController : Controller
    {
        private readonly ILogger<DomuController> _logger;

        public DomuController(ILogger<DomuController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}