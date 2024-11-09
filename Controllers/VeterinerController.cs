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
        public IActionResult MuayeneEt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KisiSec(KisiSecViewModel model)
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

            model.KisininHayvanlarininListesi = await model.KisininHayvanlarininListesiniGetir(_context, model);

            ViewBag.model = model;
            TempData["model"] = JsonConvert.SerializeObject(model);

            return View("MuayeneEt", model);
        }

        [HttpPost]
        public async Task<IActionResult> HayvanSec(string hayvanId, string tckn)
        {
            KisiSecViewModel model = new();

            var hayvanBilgileriKontrol = await model.SecilenHayvanBilgileriniGetirAsync(hayvanId, _context, tckn);

            if (!hayvanBilgileriKontrol.Item2)
                return View("BadRequest");

            ViewBag.hayvanBilgileri = hayvanBilgileriKontrol.Item1;

            var returnModel = TempData["model"] as string;
            KisiSecViewModel deserializedReturnModel = new();
            if (returnModel != null)          
                deserializedReturnModel = JsonConvert.DeserializeObject<KisiSecViewModel>(returnModel);

            ViewBag.model = deserializedReturnModel;

            return View("MuayeneEt", deserializedReturnModel);
        }











        [HttpGet]
        public IActionResult MuayeneKayitlari()
        {
            return View();
        }

    }
}
