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
        private static ItemInProgress _itemInProgress;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _effectLibrary = new EffectLibrary();
            _effectLibrary.LoadLibrary();
            _itemLibrary = new ItemLibrary();
            _itemLibrary.LoadLibrary();
            if(_itemInProgress == null)
            {
                _itemInProgress = new ItemInProgress();
            }
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

        public IActionResult EffectsLibrary()
        {
            EffectsViewModel model = new EffectsViewModel();
            model.SortEffectsLibrary(_effectLibrary);

            return View(model);
        }

        public IActionResult TheForge()
        {
            ForgeViewModel model = new ForgeViewModel();
            model.effects.SortEffectsLibrary(_effectLibrary);
            model.item = _itemInProgress;

            return View(model);
        }

        [HttpPost]
        public IActionResult ForgeChooseRarity(string fRarity, bool fMasterCraft)
        {
            _itemInProgress.SetRarity(fRarity, fMasterCraft);

            ForgeViewModel model = new ForgeViewModel();
            model.effects.SortEffectsLibrary(_effectLibrary);
            model.item = _itemInProgress;

            return View("TheForge", model);
        }

        [HttpPost]
        public IActionResult ForgeAddEffect(string fType, string fTier, string fName, string fDesc)
        {
            ForgeViewModel model = new ForgeViewModel();
            model.effects.SortEffectsLibrary(_effectLibrary);

            if (_itemInProgress.Effects.Count < _itemInProgress.EffectSlots)
            {
                Effect e = new Effect();
                e.Name = fName;
                e.Type = fType;
                e.Tier = fTier;
                e.Description = fDesc;

                bool notFound = true;

                foreach (Effect effect in _itemInProgress.Effects)
                {
                    if(effect.Name == e.Name)
                    {
                        notFound = false;
                        model.ErrorMessage = model.ErrorMessage + "Error: Item can not have more than one of the same effect. \n";
                    }
                }

                if(notFound)
                {
                    _itemInProgress.Effects.Add(e);
                }
            }
            else
            {
                model.ErrorMessage = model.ErrorMessage + "Error: Item can't have any more effects.\n";
            }


            model.item = _itemInProgress;

            return View("TheForge", model);
        }

        [HttpPost]
        public IActionResult ForgeCreateItem(string fName, string fDesc)
        {
            _itemInProgress.Name = fName;
            _itemInProgress.Description = fDesc;

            Item forgedItem = _itemInProgress.ConvertToItem();
            if( forgedItem.Validate())
            {
                _itemLibrary.itemLibrary.Add(forgedItem);
                FileIO.SerialiseItemLibrary(_itemLibrary);
            }

            ForgeViewModel model = new ForgeViewModel();
            model.effects.SortEffectsLibrary(_effectLibrary);
            model.item = new ItemInProgress();

            return View("TheForge", model);
        }

        [HttpPost]
        public IActionResult ForgeResetItem()
        {
            _itemInProgress = new ItemInProgress();

            ForgeViewModel model = new ForgeViewModel();
            model.effects.SortEffectsLibrary(_effectLibrary);
            model.item = _itemInProgress;

            return View("TheForge", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
