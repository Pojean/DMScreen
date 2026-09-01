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

        private MyItemLibrary _myItemLibrary;
        private EffectsViewModel _eModel;
        private ForgeViewModel _fModel;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            if (_effectLibrary == null)
            {
                _effectLibrary = new EffectLibrary();
                _effectLibrary.LoadLibrary();
            }

            if (_itemLibrary == null)
            {
                _itemLibrary = new ItemLibrary();
                _itemLibrary.LoadLibrary();
            }

            if (_itemInProgress == null)
            {
                _itemInProgress = new ItemInProgress();
            }

            if( _myItemLibrary == null)
            {
                _myItemLibrary = new MyItemLibrary();
                _myItemLibrary.LoadLibrary();
            }

            if (_eModel == null)
            {
                _eModel = new EffectsViewModel();
                _eModel.SortEffectsLibrary(_effectLibrary);
            }

            if (_fModel == null)
            {
                _fModel = new ForgeViewModel();
                _fModel.item = _itemInProgress;
                _fModel.effects.SortEffectsLibrary(_effectLibrary);
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
        public IActionResult SaveEffect(string fName, string fTier, string fType,string fSlot, string fAttunement, string fDesc)
        {
            Effect local = new Effect();
            local.Name = fName;
            local.Tier = fTier;
            local.Type = fType;
            local.ItemSlot = fSlot;
            local.requiresAttunement = fAttunement;
            local.Description = fDesc;

            _effectLibrary.Add(local);
            ViewBag.Message = "Effect Created.";
            return View("CreateEffect");
        }

        public IActionResult EffectsLibrary()
        {
            return View(_eModel);
        }

        public IActionResult TheForge()
        {
            _fModel.item = _itemInProgress;

            return View(_fModel);
        }

        [HttpPost]
        public IActionResult ForgeChooseRarity(string fRarity, bool fMasterCraft)
        {
            _itemInProgress.SetRarity(fRarity, fMasterCraft);

            return View("TheForge", _fModel);
        }

        [HttpPost]
        public IActionResult ForgeAddEffect(string fType, string fTier, string fName, string fDesc)
        {
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
                    if (effect.Name == e.Name)
                    {
                        notFound = false;
                        _fModel.ErrorMessage = _fModel.ErrorMessage + "Error: Item can not have more than one of the same effect. \n";
                    }
                }

                if (notFound)
                {
                    _itemInProgress.Effects.Add(e);
                }
            }
            else
            {
                _fModel.ErrorMessage = _fModel.ErrorMessage + "Error: Item can't have any more effects.\n";
            }


            _fModel.item = _itemInProgress;

            return View("TheForge", _fModel);
        }

        [HttpPost]
        public IActionResult ForgeSortEffects(string fSRarity, string fSSearch)
        {
            ForgeViewModel localFilteredEffects = new ForgeViewModel();
            localFilteredEffects.item = _itemInProgress;
            localFilteredEffects.effects.SortedEffects = _eModel.SortByConditions(fSRarity, fSSearch);

            return View("TheForge", localFilteredEffects);
        }

        [HttpPost]
        public IActionResult ForgeCreateItem(string fName, string fDesc)
        {
            _itemInProgress.Name = fName;
            _itemInProgress.Description = fDesc;


            Item forgedItem = _itemInProgress.ConvertToItem();
            if (forgedItem.Validate())
            {
                _itemLibrary.itemLibrary.Add(forgedItem);
                FileIO.SerialiseItemLibrary(_itemLibrary);
            }

            _fModel.item = new ItemInProgress();

            return View("TheForge", _fModel);
        }

        [HttpPost]
        public IActionResult ForgeResetItem()
        {
            _itemInProgress = new ItemInProgress();

            _fModel.item = _itemInProgress;

            return View("TheForge", _fModel);
        }

        public IActionResult ItemHistoryView()
        {

            return View(_itemLibrary);
        }

        public IActionResult ItemView(string fName)
        {
            Item itemToView = new Item();
            for (int i = 0; i < _itemLibrary.itemLibrary.Count; i++)
            {
                if (_itemLibrary.itemLibrary[i].Name == fName)
                {
                    itemToView = _itemLibrary.itemLibrary[i];
                    break;
                }
            }
            return View(itemToView);
        }

        public IActionResult MyItems()
        {

            return View(_myItemLibrary);
        }

        public IActionResult AddToMyItems(string fName)
        {
            Item currentItem = new Item();
            currentItem = _itemLibrary.itemLibrary.FirstOrDefault(x => x.Name == fName);

            _myItemLibrary.itemLibrary.Add(currentItem);
            _myItemLibrary.SaveLibrary();

            ViewBag.Message = "Item added";
            return View("ItemView", currentItem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
