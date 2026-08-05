using DMScreen.Models;
using DMScreen.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DMScreen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FileIO _fileIO;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _fileIO = new FileIO();
        }

        public IActionResult Index()
        {
            Item i = new Item();
            i.Name = "Test item";
            i.Description = "This item is crafted by the finest codesmiths, it's handle is made of the purest ones and zeroes";
            i.Effects.Add("- While wearing this item, you look really really cool");
            i.Effects.Add("- As an action, hold the item aloft and yell out '404' and it will dissapear!");

            _fileIO.SerialiseItem(i);
            
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
