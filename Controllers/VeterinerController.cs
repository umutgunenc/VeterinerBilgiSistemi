using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.Veteriner;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;



namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize(Roles = "VETERİNER,VETERINER")]
    public class VeterinerController : Controller
    {
        private readonly VeterinerDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public VeterinerController(VeterinerDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult VeterinerIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Muayene()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KisiSec(KisiSecVeterinerViewModel model)
        {
            KisiSecValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }

                return View("MuayeneEt", model);
            }

            model.KisininHayvanlarininListesi = await model.KisininHayvanlarininListesiniGetirAsync(_context, model);

            ViewBag.model = model;
            TempData["model"] = JsonConvert.SerializeObject(model);

            return View("Muayene", model);
        }

        [HttpPost]
        public async Task<IActionResult> HayvanSec(string hayvanId, string tckn)
        {
            KisiSecVeterinerViewModel model = new();

            var hayvanBilgileriKontrol = await model.SecilenHayvanBilgileriniGetirAsync(hayvanId, _context, tckn);

            if (!hayvanBilgileriKontrol.Item2)
                return View("BadRequest");

            ViewBag.hayvanBilgileri = hayvanBilgileriKontrol.Item1;

            KisiSecVeterinerViewModel returnModel = new();
            returnModel.InsanTckn = tckn;
            returnModel.KisininHayvanlarininListesi = await returnModel.KisininHayvanlarininListesiniGetirAsync(_context, returnModel);

            ViewBag.model = returnModel;

            return View("Muayene", returnModel);
        }


        [HttpGet]
        public async Task<IActionResult> MuayeneEt(string hayvanId)
        {
            MuayeneEtViewModel model = new();
            model.Hayvan = await model.MuayeneOlacakHayvaniGetirAsync(hayvanId, _context);
            model.StokLarListesi = await model.StoklarinListesiniGetirAsync(_context);
            model.HastalikListesi = await model.HastaliklarListesiniGetirAsync(_context);
            model.KanTestleriListesi = await model.MuayenedeYapilacakKanTestlerininListeisiniGetirAsync(_context);
            model.Hekim = await _userManager.GetUserAsync(User);

            if (model.Hayvan == null)
                return View("BadRequest");


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MuayeneEt(MuayeneEtViewModel model)
        {
            return View();
        }
        





        [HttpGet]
        public IActionResult MuayeneKayitlari()
        {
            return View();
        }

    }
}
