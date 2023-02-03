using EventStreamingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using EventStreamingPlatform.Data;
using Microsoft.Extensions.Logging;


namespace EventStreamingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("MySessionKey", 123);
            return View();
        }

        public IActionResult Privacy()
        {
            var sessionValue = HttpContext.Session.GetInt32("MySessionKey");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
   
    }
}