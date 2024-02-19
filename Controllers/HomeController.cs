using BabyNames.BAL;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BabyNames.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string controllerName = "SEC_User";
            string actionName = "Login";
            string areaName = "SEC_User";

            // Return the view from the specified controller and area
            return View($"~/Areas/{areaName}/Views/{controllerName}/{actionName}.cshtml");
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