using Diplom.ForDb;
using Diplom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Diplom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = new MyDbContext();
        }

        public IActionResult Index()
        {
            return View(_dbContext.MainPageGames.ToList());
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