using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
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

                return View("Muayene", model);
            }

            model.KisininHayvanlarininListesi = await model.KisininHayvanlarininListesiniGetirAsync(_context, model);

            ViewBag.model = model;
            //TempData["model"] = JsonConvert.SerializeObject(model);

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
            model.HayvanId = model.Hayvan.HayvanId;
            model.StokLarListesi = await model.StoklarinListesiniGetirAsync(_context);
            model.HastalikListesi = await model.HastaliklarListesiniGetirAsync(_context);
            model.KanTestleriListesi = await model.MuayenedeYapilacakKanTestlerininListeisiniGetirAsync(_context);
            model.HekimId = (await _userManager.GetUserAsync(User)).Id;
            model.Imza = Signature.CreateSignature(model.HekimId.ToString(), model.HayvanId.ToString());

            if (model.Hayvan == null)
                return View("BadRequest");


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MuayeneEt(MuayeneEtViewModel model)
        {
            if (!Signature.VerifySignature(model.HekimId.ToString(), model.HayvanId.ToString(), model.Imza))
                return View("BadRequest");

            MuayeneEtValidators validator = new();
            ValidationResult result = validator.Validate(model);


            model.Hayvan = await model.MuayeneOlacakHayvaniGetirAsync(model.HayvanId.ToString(), _context);
            model.StokLarListesi = await model.StoklarinListesiniGetirAsync(_context);
            model.HastalikListesi = await model.HastaliklarListesiniGetirAsync(_context);
            model.KanTestleriListesi = await model.MuayenedeYapilacakKanTestlerininListeisiniGetirAsync(_context);
            model.Hekim = await _userManager.GetUserAsync(User);
            model.Imza = Signature.CreateSignature(model.HekimId.ToString(), model.Hayvan.HayvanId.ToString());

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(model);
            }

            Muayene muayene = new();
            muayene.HekimId = model.HekimId;
            muayene.HayvanId = model.HayvanId;
            muayene.MuayeneTarihi = DateTime.Now;
            muayene.SonrakiMuayeneTarihi = model.SonrakiMuayeneTarihi;
            muayene.Aciklama = model.Aciklama.ToUpper();
            muayene.HastalikId = model.HastalikId;

            await _context.Muayeneler.AddAsync(muayene);
            await _context.SaveChangesAsync();

            var stokHarekler = model.MuayenedeKullanilanStoklar
                .Where(x => x.SeciliMi == true)
                .Select(x => new StokHareket
                {
                    StokId = x.StokId,
                    SatisTarihi = muayene.MuayeneTarihi,
                    StokHareketTarihi = muayene.MuayeneTarihi,
                    StokCikisAdet = x.StokCikisAdet,
                    CalisanId = muayene.HekimId,
                    MuayeneId = muayene.MuayeneId
                })
            .ToList();

            await _context.StokHareketler.AddRangeAsync(stokHarekler);
            await _context.SaveChangesAsync();

            var kanTestleri = model.MuayendeYapilanKanTestleri
                .Where(x => x.SeciliMi == true)
                .Select(x => new KanTestiMuayene
                {
                    KanDegerleriId = x.KanDegerleriId,
                    KanDegeriValue = x.KanDegeriValue,
                    MuayeneId = muayene.MuayeneId

                })
                .ToList();

            await _context.KanTestiMuayene.AddRangeAsync(kanTestleri);
            await _context.SaveChangesAsync();

            TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";

            return View("Muayene");
        }
        





        [HttpGet]
        public IActionResult MuayeneKayitlari()
        {
            return View();

        }

    }
}
