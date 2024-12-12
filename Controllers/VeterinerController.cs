using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Classes;
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
        private readonly HttpClient _httpClient;

        public VeterinerController(VeterinerDBContext context, UserManager<AppUser> userManager, HttpClient httpClient)
        {
            _context = context;
            _userManager = userManager;
            _httpClient = httpClient;
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

            model.KisininHayvanlarininListesi = await model.KisininHayvanlarininListesiniGetirAsync(_context);

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
            returnModel.KisininHayvanlarininListesi = await returnModel.KisininHayvanlarininListesiniGetirAsync(_context);

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

        //TODO validation mesajları için alert ekle
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
                TempData["modelError"] = "Forma Girilen Bazı Bilgiler Hatalı Girildi./nGirilen Bilgileri Kontrol Ediniz.";
                return View(model);
            }

            Muayene muayene = new();
            muayene.HekimId = model.HekimId;
            muayene.HayvanId = model.HayvanId;
            muayene.MuayeneTarihi = DateTime.Now;
            muayene.SonrakiMuayeneTarihi = model.SonrakiMuayeneTarihi;
            if (model.Aciklama != null)
                muayene.Aciklama = model.Aciklama.ToUpper();
            muayene.HastalikId = model.HastalikId;

            await _context.Muayeneler.AddAsync(muayene);
            await _context.SaveChangesAsync();

            if (model.MuayenedeKullanilanStoklar != null)
            {
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
            }

            if (model.MuayendeYapilanKanTestleri != null)
            {
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
            }



            List<KanTestiSonucu> apiRequest = new();
            foreach (var kanTesti in model.MuayendeYapilanKanTestleri.Where(x => x.SeciliMi == true))
            {
                apiRequest.Add(new KanTestiSonucu
                {
                    KanDegerleriId = kanTesti.KanDegerleriId,
                    TestSonucu = kanTesti.KanDegeriValue
                });
            }

            try
            {
                string jsonRequest = JsonSerializer.Serialize(apiRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("http://127.0.0.1:5002/api/TahminYap", content);


                string responseMessage = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonSerializer.Deserialize<YapayZekaTahminResponseModel>(responseMessage);

                if (jsonResponse != null && jsonResponse.status == "success")
                {
                    muayene.YapayZekaTahminId = jsonResponse.tahminId;

                    _context.Muayeneler.Update(muayene);
                    await _context.SaveChangesAsync();
                    if (muayene.HastalikId != muayene.YapayZekaTahminId)
                    {
                        ViewBag.tahminEdilenHastalik = jsonResponse.tahmin.ToUpper();
                    }

                    TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                    return View("Muayene");

                }
                else
                {
                    TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                    TempData["YapayZekaError"] = "Muayene bilgileri kaydedildi, Loglama işlemi başarısız oldu.";
                    return View("Muayene");
                }

            }
            catch (Exception)
            {
                TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                TempData["YapayZekaError"] = "Muayene bilgileri kaydedildi, Loglama işlemi başarısız oldu.";
                return View("Muayene");
            }

        }

        [HttpGet]
        public async Task<IActionResult> MuayeneKayitlari(MuayeneKayitlariViewModel model)
        {
            model.MevcutSayfa = model.MevcutSayfa <= 0 ? 1 : model.MevcutSayfa;

            model.ilkTarih = model.ilkTarih ?? null;
            model.sonTarih = model.sonTarih ?? DateTime.Now;


            model.HekimAdi = model.HekimAdi?.Trim() ?? string.Empty;
            model.HayvanAdi = model.HayvanAdi?.Trim() ?? string.Empty;
            model.MuayenelerListesi = await model.MuayeneKayitlariniGetirAsync(_context);

            model.SonSayfaNumarasi = model.ToplamSayfaSayisiniGetir();
            model.SonSayfaNumarasi = model.SonSayfaNumarasi <= 0 ? 1 : model.SonSayfaNumarasi;

            model.MevcutSayfa = model.MevcutSayfa > model.SonSayfaNumarasi ? model.SonSayfaNumarasi : model.MevcutSayfa;


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> MuayeneNo(int id)
        {
            MuayeneNoViewModel model = new();

            model.Muayene = await model.MuayeneyiGetirAsync(id, _context);
            model.HayvanId = model.Muayene.HayvanId;
            model.Imza = Signature.CreateSignature(model.HayvanId.ToString(), model.Muayene.MuayeneId.ToString());
            model.KullanilanIlaclar = await model.IlaclarListesiniGetirAsync(_context);
            model.YapilanKanTestleri = await model.KanTestleriListesiniGetirAsync(_context);
            model.Hastaliklar = await model.HastalikListesiniGetirAsync(_context);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MuayeneNo(MuayeneNoViewModel model)
        {
            if (!Signature.VerifySignature(model.HayvanId.ToString(), model.Muayene.MuayeneId.ToString(), model.Imza))
                return View("BadRequest");

            MuayeneNoValidators validator = new();
            ValidationResult result = validator.Validate(model);

            model.Hayvan = await _context.Hayvanlar.FindAsync(model.HayvanId);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var returnModel = new MuayeneNoViewModel();

                returnModel.Muayene = model.Muayene;
                returnModel.HayvanId = model.Muayene.HayvanId;
                returnModel.Imza = Signature.CreateSignature(model.HayvanId.ToString(), model.Muayene.MuayeneId.ToString());
                returnModel.KullanilanIlaclar = await returnModel.IlaclarListesiniGetirAsync(_context);
                returnModel.YapilanKanTestleri = await returnModel.KanTestleriListesiniGetirAsync(_context);
                returnModel.Hastaliklar = await returnModel.HastalikListesiniGetirAsync(_context);

                for (int i = 0; i < returnModel.KullanilanIlaclar.Count; i++)
                {
                    returnModel.KullanilanIlaclar[i].Stok.StokHareketleri.FirstOrDefault().StokCikisAdet = model.KullanilanIlaclar[i].StokCikisAdet;
                    if (model.KullanilanIlaclar[i].YapildiMi)
                        returnModel.KullanilanIlaclar[i].YapildiMi = true;
                    else
                        returnModel.KullanilanIlaclar[i].YapildiMi = false;
                }
                for (int i = 0; i < returnModel.YapilanKanTestleri.Count; i++)
                {
                    //if (returnModel.YapilanKanTestleri[i].KanTesti.Muayeneler == null)
                    //    returnModel.YapilanKanTestleri[i].KanTesti.Muayeneler = new List<KanTestiMuayene>();

                    if (returnModel.YapilanKanTestleri[i].KanDegerleri.Muayeneler.FirstOrDefault() == null)
                    {
                        returnModel.YapilanKanTestleri[i].KanDegerleri.Muayeneler.Add(new KanTestiMuayene
                        {
                            KanDegerleriId = model.YapilanKanTestleri[i].KanDegerleriId,
                            KanDegeriValue = model.YapilanKanTestleri[i].KanDegeriValue,
                            MuayeneId = returnModel.Muayene.MuayeneId,
                        });
                    }

                    var mevcutMuayene = returnModel.YapilanKanTestleri[i].KanDegerleri.Muayeneler
                        .FirstOrDefault(x => x.Muayene != null && x.Muayene.MuayeneId == model.Muayene.MuayeneId);

                    if (mevcutMuayene == null)
                    {
                        mevcutMuayene = new KanTestiMuayene
                        {
                            KanDegerleriId = model.YapilanKanTestleri[i].KanDegerleriId,
                            MuayeneId = model.Muayene.MuayeneId,
                        };

                        returnModel.YapilanKanTestleri[i].KanDegerleri.Muayeneler.Add(mevcutMuayene);
                    }

                    mevcutMuayene.KanDegeriValue = model.YapilanKanTestleri[i].KanDegeriValue;

                    var hedefMuayene = returnModel.YapilanKanTestleri[i].KanDegerleri.Muayeneler
                        .FirstOrDefault(x => x.Muayene != null && x.Muayene.MuayeneId == model.Muayene.MuayeneId);

                    if (hedefMuayene != null)
                        hedefMuayene.KanDegeriValue = model.YapilanKanTestleri[i].KanDegeriValue;

                    returnModel.YapilanKanTestleri[i].SecildiMi = model.YapilanKanTestleri[i].SecildiMi;
                }

                TempData["Hata"] = "Form Doldurulurken Yanlis Bir Deger Girildi.\nGirilen Degerleri Kontrol Ediniz.";
                return View(returnModel);
            }

            var muayene = model.Muayene;
            if (muayene.Aciklama != null)
                muayene.Aciklama = muayene.Aciklama.ToUpper();

            muayene.MuayeneTarihi = DateTime.Now;
            var hekim = await _userManager.GetUserAsync(User);
            muayene.HekimId = hekim.Id;
            muayene.HayvanId = model.HayvanId;

            _context.Update(muayene);
            await _context.SaveChangesAsync();

            var stokHarektler = await _context.StokHareketler
                .Where(sh => sh.MuayeneId == muayene.MuayeneId)
                .ToListAsync();


            if (model.KullanilanIlaclar != null)
            {
                List<StokHareket> yeniStokListesi = new();
                foreach (var stokHareket in model.KullanilanIlaclar.Where(ki => ki.YapildiMi == true))
                {
                    var yeniStokHareket = new StokHareket();
                    yeniStokHareket.StokCikisAdet = stokHareket.StokCikisAdet;
                    yeniStokHareket.SatisTarihi = muayene.MuayeneTarihi;
                    yeniStokHareket.CalisanId = hekim.Id;
                    yeniStokHareket.MuayeneId = muayene.MuayeneId;
                    yeniStokHareket.StokHareketTarihi = muayene.MuayeneTarihi;
                    yeniStokHareket.StokId = stokHareket.Stok.Id;

                    yeniStokListesi.Add(yeniStokHareket);
                }

                _context.StokHareketler.RemoveRange(stokHarektler);
                await _context.SaveChangesAsync();

                await _context.StokHareketler.AddRangeAsync(yeniStokListesi);
                await _context.SaveChangesAsync();

            }

            if (model.YapilanKanTestleri != null)
            {
                var eskiKanDegerleriListesi = await _context.KanTestiMuayene
                .Where(ktm => ktm.MuayeneId == muayene.MuayeneId)
                .ToListAsync();

                List<KanTestiMuayene> yeniKanTestiListesi = new();

                foreach (var kanTesti in model.YapilanKanTestleri.Where(x => x.SecildiMi == true))
                {
                    var yeniKanTesti = new KanTestiMuayene();
                    yeniKanTesti.MuayeneId = muayene.MuayeneId;
                    yeniKanTesti.KanDegeriValue = kanTesti.KanDegeriValue;
                    yeniKanTesti.KanDegerleriId = kanTesti.KanDegerleri.KanDegerleriId;

                    yeniKanTestiListesi.Add(yeniKanTesti);
                }

                _context.KanTestiMuayene.RemoveRange(eskiKanDegerleriListesi);
                await _context.SaveChangesAsync();

                await _context.KanTestiMuayene.AddRangeAsync(yeniKanTestiListesi);
                await _context.SaveChangesAsync();
            }

            List<KanTestiSonucu> apiRequest = new();
            foreach (var kanTesti in model.YapilanKanTestleri.Where(x => x.SecildiMi == true))
            {
                apiRequest.Add(new KanTestiSonucu
                {
                    KanDegerleriId = kanTesti.KanDegerleri.KanDegerleriId,
                    TestSonucu = kanTesti.KanDegeriValue
                });
            }

            try
            {
                string jsonRequest = JsonSerializer.Serialize(apiRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("http://127.0.0.1:5002/api/TahminYap", content);


                string responseMessage = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonSerializer.Deserialize<YapayZekaTahminResponseModel>(responseMessage);

                if (jsonResponse != null && jsonResponse.status == "success")
                {
                    muayene.YapayZekaTahminId = jsonResponse.tahminId;

                    _context.Muayeneler.Update(muayene);
                    await _context.SaveChangesAsync();
                    if (muayene.HastalikId != muayene.YapayZekaTahminId)
                        ViewBag.tahminEdilenHastalik = jsonResponse.tahmin.ToUpper();

                    TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                    return View("Muayene");

                }
                else
                {
                    TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                    TempData["YapayZekaError"] = "Muayene bilgileri kaydedildi, Loglama işlemi başarısız oldu.";
                    return View("Muayene");
                }

            }
            catch (Exception)
            {
                TempData["MuayeneEdildi"] = $"{model.Hayvan.HayvanAdi.ToUpper()} isimli hayvanin muayenesi tamamlandı.";
                TempData["YapayZekaError"] = "Muayene bilgileri kaydedildi, Loglama işlemi başarısız oldu.";
                return View("Muayene");
            }

        }


    }
}


