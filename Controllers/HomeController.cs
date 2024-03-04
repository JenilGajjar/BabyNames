using BabyNames.BAL;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyNames.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return RedirectToAction("Index", "SEC_User", new { area = "SEC_User" });

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

        [Route("/StatusCodeError/{StatusCode}")]
        public IActionResult Error(int StatusCode)
        {
            
            return View("Index");
        }
    }
}