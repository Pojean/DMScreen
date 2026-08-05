using DMScreen.Models;
using DMScreen.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DMScreen.Controllers
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
            EffectLibrary elib = new EffectLibrary();
            elib.effectsLibrary.Add(new Effect { Type = "Utility", Tier = "Uncommon", Description = "As an action, you can conjure a 20-foot radius sphere of errors, all creatures inside the sphere get very frustrated." });
            elib.SaveLibrary();

            EffectLibrary elib2 = new EffectLibrary();
            elib2.LoadLibrary();

            ItemLibrary ilib = new ItemLibrary();
            ilib.itemLibrary.Add(new Item
            {
                Name = "The Ingenburger Legacy",
                Rarity = "Very Rare",
                EffectSlots = 4,
                Description = "The Ingenburger Legacy is the pinnacle of spatulas. Able to flip any burger patty, no matter how stuck to the grill it is.",
                Effects = { new Effect {Name = "The Flip", Tier ="Very Rare", Type = "Offense", Description = "As part of an attack, slide the spatula under an enemy, flipping them prone."}, new Effect {Name = "The Slide", Tier = "Rare", Type = "Defense",
                Description = "When hit by an attack, as a reaction, use the spatual to parry the blow, ignoring the hit as if it didn't land."}}
            });
            ilib.SaveLibrary();

            ItemLibrary ilib2 = new ItemLibrary();
            ilib2.LoadLibrary();
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
