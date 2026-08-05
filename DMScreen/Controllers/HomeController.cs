using DMScreen.Models;
using DMScreen.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DMScreen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EffectLibrary _effectLibrary;
        private ItemLibrary _itemLibrary;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _effectLibrary = new EffectLibrary();
            _effectLibrary.LoadLibrary();
            _itemLibrary = new ItemLibrary();
            _itemLibrary.LoadLibrary();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEffect()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveEffect(string fName, string fTier, string fType, string fDesc)
        {
            Effect local = new Effect();
            local.Name = fName;
            local.Tier = fTier;
            local.Type = fType;
            local.Description = fDesc;

            _effectLibrary.Add(local);
            ViewBag.Message = "Effect Created.";
            return View("CreateEffect");
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
