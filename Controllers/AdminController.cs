using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using FluentValidation;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VeterinerBilgiSistemi.Models.Validators.Admin;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IO;
using System.Collections.Generic;



namespace VeterinerBilgiSistemi.Controllers
{
    //TODO  stok detaylarında ortalama alış fiyatı ve stok fiyatı görünüyor
    //bu alanları kaldır
    //stokhareketdetay listesinde ortalama alış fiyatını sil
    //Stok ile ilgili viewlerde kategori listesi şeklinde bir ifade var, onu kategoriler olarak değiştir
    //stok detay sayfasında alışlar ve satışlar gözükmüyor, düzelt
    //Sosyal medya duzenle sayfası yapılmamış

    [Authorize(Roles = "ADMIN,ADMİN,admin,admın")]
    public class AdminController : Controller
    {
        private readonly VeterinerDBContext _veterinerDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(VeterinerDBContext veterinerDbContext, UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _veterinerDbContext = veterinerDbContext;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RenkEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RenkEkle(RenkEkleViewModel model)
        {

            RenkEkleValidators renkvalidator = new();
            ValidationResult result = renkvalidator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            model.RenkAdi = model.RenkAdi.ToUpper();

            await _veterinerDbContext.Renkler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["Success"] = $"{model.RenkAdi} rengi eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> RenkSil()
        {
            RenkSilViewModel model = new();
            model.RenklerListesi = await model.RenklerListesiniGetirAsync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RenkSil(RenkSilViewModel model)
        {
            RenkSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }
                model.RenklerListesi = await model.RenklerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }
            var silinecekRenk = await model.SilinecekRengiGetirAsync(model, _veterinerDbContext);
            _veterinerDbContext.Renkler.Remove(silinecekRenk);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["RenkSilindi"] = $"{silinecekRenk.RenkAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult CinsEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CinsEkle(CinsEkleViewModel model)
        {
            CinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            model.CinsAdi = model.CinsAdi.ToUpper();

            await _veterinerDbContext.Cinsler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["CinsEklendi"] = $"{model.CinsAdi} cinsi eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsSil()
        {
            CinsSilViewModel model = new();
            model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CinsSil(CinsSilViewModel model)
        {

            CinsSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);
                return View(model);
            }

            var silinecekCins = await model.SilinecekCinsiGetir(model, _veterinerDbContext);

            _veterinerDbContext.Cinsler.Remove(silinecekCins);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["CinsSilindi"] = $"{silinecekCins.CinsAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult TurEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TurEkle(TurEKleViewModel model)
        {
            TurEkleValidators validator = new TurEkleValidators();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            model.TurAdi = model.TurAdi.ToUpper();

            await _veterinerDbContext.Turler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurEklendi"] = $"{model.TurAdi} türü eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> TurSil()
        {
            TurSilViewModel model = new();
            model.TurListesi = await model.TurListesiniGetirASync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> TurSil(TurSilViewModel model)
        {
            TurSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }
                model.TurListesi = await model.TurListesiniGetirASync(_veterinerDbContext);
                return View(model);

            }
            var silinecekTur = await model.SilinecekTuruGetirAsync(model, _veterinerDbContext);

            _veterinerDbContext.Turler.Remove(silinecekTur);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TurSilindi"] = $"{silinecekTur.TurAdi} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurEslestir()
        {
            var model = new CinsTurEslestirViewModel();

            model.TurlerListesi = await model.TurlerListesiGetirAsync(_veterinerDbContext);
            model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CinsTurEslestir(CinsTurEslestirViewModel model)
        {
            TurCinsEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model.TurlerListesi = await model.TurlerListesiGetirAsync(_veterinerDbContext);
                model.CinslerListesi = await model.CinslerListesiGetirAsync(_veterinerDbContext);

                return View(model);
            }


            await _veterinerDbContext.CinsTur.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            model.Cins = await model.SecilenCinsiGetirAsync(_veterinerDbContext, model);
            model.Tur = await model.SecilenTuruGetirAsync(_veterinerDbContext, model);

            TempData["CinsTurEklendi"] = $"{model.Cins.CinsAdi} cinsi ve {model.Tur.TurAdi} türü eşleştirildi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> CinsTurEslesmeKaldir()
        {
            var model = new CinsTurEslesmeKaldirViewModel();
            model.CinslerTurlerListesi = await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext);

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CinsTurEslesmeKaldir(CinsTurEslesmeKaldirViewModel model)
        {

            TurCinsSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model.CinslerTurlerListesi = await model.CinslerTurlerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            model.EslemesiKaldiralacakCinstur = await model.EslesmesiKaldirilacakCinsTuruGetirAsync(model, _veterinerDbContext);

            var cinsAdi = await model.EslesmesiKaldirilacakCinsAdiniGetirAsync(model, _veterinerDbContext);
            var turAdi = await model.EslesmesiKaldirilacakTurAdiniGetirAsync(model, _veterinerDbContext);


            _veterinerDbContext.CinsTur.Remove(model.EslemesiKaldiralacakCinstur);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["EslemeKaldiridi"] = $"{cinsAdi} cinsi ve {turAdi} türü arasındaki eşleştirme kaldırıldı.";

            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult RolEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RolEkle(RolEkleViewModel model)
        {

            RolValidators rolValidator = new RolValidators();
            ValidationResult result = rolValidator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }
            model.Name = model.Name.ToUpper();

            await _veterinerDbContext.Roles.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["RolEklendi"] = $"{model.Name.ToUpper()} türünde bir rol başarı ile eklendi eklendi";
            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> RolSil()
        {
            var model = new RolSilViewModel();

            model.RollerListesi = await model.RollerListesiniGetir(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RolSil(RolSilViewModel model)
        {

            RolSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                model.RollerListesi = await model.RollerListesiniGetir(_veterinerDbContext);

                return View(model);
            }
            model.SilinecekRol = await model.SilinecekRoluGetir(_veterinerDbContext, model);
            _veterinerDbContext.Roles.Remove(model.SilinecekRol);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["RolSilindi"] = $"{model.SilinecekRol.Name} başarı ile silindi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> KisiEkle()
        {
            var model = new KisiEkleViewModel();
            model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> KisiEkle(KisiEkleViewModel model)
        {
            model.Kullanici = await model.KisiOlusturAsync(_veterinerDbContext, model);

            KisiEkleValidators validator = new KisiEkleValidators();
            ValidationResult result = validator.Validate(model.Kullanici);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            var createResult = await _userManager.CreateAsync(model.Kullanici, model.KullaniciSifresi);
            await _veterinerDbContext.SaveChangesAsync(); // Kullaniciya rol atayabilmek için kullanıcıyı veritabanına kaydetmemiz gerekiyor.

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }
            model.KullaniciRolu = await model.KisininRolunuGetirAsync(_veterinerDbContext, model);

            await _veterinerDbContext.UserRoles.AddAsync(model.KullaniciRolu);

            if (await _veterinerDbContext.SaveChangesAsync() > 0)
            {
                var loginUrl = Url.Action("Login", "Account", null, Request.Scheme);
                var kullaniciAdSoyad = model.InsanAdi.ToUpper() + " " + model.InsanSoyadi.ToUpper();
                var tarih = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                var rolAdi = await _veterinerDbContext.Roles
                    .Where(x => x.Id == model.RolId)
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync();

                string mailMessage = $@"
                <!DOCTYPE html>
                <html lang='tr'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Hoş Geldiniz!</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            margin: 0;
                            padding: 0;
                            font-size: 1.2rem;
                            background-color: #f8f9fa;
                        }}
                        h1 {{
                            font-size: 1.8rem;
                            text-align: center;
                            margin-bottom: 20px;
                            color: #fff;
                        }}
                        .container {{
                            padding: 20px;
                            max-width: 600px;
                            margin: auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
                        }}
                        .header {{
                            background-color: #343a40;
                            color: white;
                            padding: 10px;
                            border-radius: 8px 8px 0 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .credentials {{
                            background-color: #f9f9f9;
                            border-left: 4px solid #007bff;
                            padding: 10px;
                            margin-bottom: 20px;
                            font-size: 1.2rem;
                        }}
                        a.button {{
                            display: inline-block;
                            background-color: #007bff;
                            color: white !important;
                            padding: 10px 20px;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                            text-align: center;
                            margin-top: 10px;
                            background-color: #6c757d;
                        }}
                        a.button:hover {{
                            background-color: #5a6268;
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            font-size: 12px;
                            color: #777;
                            border-top: 1px solid #e9ecef;
                            padding-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Hoş Geldiniz!</h1>
                        </div>
                        <div class='content'>
                            <p>Sayın {kullaniciAdSoyad}, {tarih} tarihinde sisteme {rolAdi} görevinde başarıyla üye oldunuz. Aşağıda giriş bilgileriniz yer almaktadır:</p>
                            <div class='credentials'>
                                <p><strong>Kullanıcı Adı:</strong> {model.UserName}</p>
                                <p><strong>Şifre:</strong> {model.KullaniciSifresi}</p>
                            </div>
                            <p style='text-align:center;'>
                                <a href='{loginUrl}' class='button'>Giriş Yap</a>
                            </p>
                        </div>
                        <div class='footer'>
                            <p>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayın.</p>
                        </div>
                    </div>
                </body>
                </html>";

                try
                {
                    await _emailSender.SendEmailAsync(model.Email, "Veteriner Bilgi Sistemi'ne Hoş Geldiniz!", mailMessage);
                    TempData["CalısanEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli calışan {rolAdi.ToUpper()} görevi ile sisteme kaydedildi. Kullanıcı adı ve şifresi {model.Email.ToUpper()} adresine gönderildi.";
                    return RedirectToAction();

                }
                catch (Exception ex)
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı." + " " + ex.Message;

                    _veterinerDbContext.Users.Remove(model.Kullanici);
                    await _veterinerDbContext.SaveChangesAsync();
                    model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);
                    return View(model);
                }

            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult KisiSec()
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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            KisiDuzenleViewModel kisiDuzenleViewModel = new();
            kisiDuzenleViewModel.SecilenKisi = await kisiDuzenleViewModel.SecilenKisiyiGetirAsync(_veterinerDbContext, model);
            kisiDuzenleViewModel.Signature = Signature.CreateSignature(kisiDuzenleViewModel.Id.ToString(), kisiDuzenleViewModel.InsanTckn);

            ViewBag.model = kisiDuzenleViewModel;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> KisiDuzenle(KisiDuzenleViewModel model)
        {
            if (!Signature.VerifySignature(model.Id.ToString(), model.InsanTckn, model.Signature))
                return View("BadRequest");

            KisiDuzenleValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.RollerListesi = await model.RollerListesiniGetirAsync(_veterinerDbContext);
                ViewBag.model = model;
                return View("KisiSec");
            }

            model.EskiRol = await model.KullanicininEskiRolunuGetirAsync(_veterinerDbContext, model);
            model.YeniRol = model.KullanicininYeniRolunuGetir(model);


            if (model.EskiRol.RoleId != model.YeniRol.RoleId)
            {
                _veterinerDbContext.UserRoles.Remove(model.EskiRol);
                await _veterinerDbContext.UserRoles.AddAsync(model.YeniRol);
            }

            model.UpdateOlacakKullanici = await model.UpdateOlacakKullaniciyiGetirAsync(_veterinerDbContext, model);
            _veterinerDbContext.Users.Update(model.UpdateOlacakKullanici);

            await _veterinerDbContext.SaveChangesAsync();

            TempData["KisiGuncellendi"] = $"{model.InsanAdi} {model.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
            return View("KisiSec");
        }

        [HttpGet]
        public IActionResult KisileriListele(int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;

            var model = new KisileriListeleViewModel();
            model.Kisiler = model.KisiListesiniGetir(_veterinerDbContext);


            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(model.Kisiler, sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            ViewBag.ToplamKayit = model.Kisiler.Count();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> KisileriListele(string secilenKisiTckn, int sayfaNumarasi = 1)
        {
            int sayfadaGosterilecekKayitSayisi = 4;
            KisileriListeleViewModel kisiler = new();

            kisiler.SecilenKisi = await kisiler.SecilenKisiyiGetirAsync(secilenKisiTckn, _veterinerDbContext);


            if (kisiler.SecilenKisi == null)
                return View("BadRequest");


            ViewBag.SecilenKisi = kisiler.SecilenKisi;


            var viewModel = SayfalamaListesi<KisileriListeleViewModel>.Olustur(kisiler.KisiListesiniGetir(_veterinerDbContext), sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KategoriEkle(KategoriViewModel model)
        {
            KategoriEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            model.KategoriAdi = model.KategoriAdi.ToUpper();

            await _veterinerDbContext.Kategoriler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["KategoriEklendi"] = $" {model.KategoriAdi} isimli kategori başarı ile eklendi";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> KategoriSil()
        {
            KategoriSilViewModel model = new();
            model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> KategoriSil(KategoriSilViewModel model)
        {
            KategoriSilValidator validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);

                return View(model);
            }

            model.SilinecekKategori = await model.SilinecekKategoriyiGetirAsync(_veterinerDbContext, model);
            _veterinerDbContext.Kategoriler.Remove(model.SilinecekKategori);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["KategoriSilindi"] = $"{model.SilinecekKategori.KategoriAdi} başarı ile silindi.";
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult BirimEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BirimEkle(BirimEkleViewModel model)
        {
            BirimEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            model.BirimAdi = model.BirimAdi.ToUpper();

            await _veterinerDbContext.Birimler.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["BirimEklendi"] = $"{model.BirimAdi} isimli birim başarı ile eklendi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> BirimSil()
        {
            BirimSilViewModel model = new();
            model.BirimLerListesi = await model.BirimlerListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BirimSil(BirimSilViewModel model)
        {
            BirimSilValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.BirimLerListesi = await model.BirimlerListesiniGetirAsync(_veterinerDbContext);
                return View();
            }

            model.SilinecekBirim = await model.SilinecekBirimGetirAsync(_veterinerDbContext, model);

            _veterinerDbContext.Birimler.Remove(model.SilinecekBirim);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["BirimSilindi"] = $"{model.SilinecekBirim.BirimAdi} başarı ile silindi.";
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> StokKartiOlustur()
        {
            StokKartiOlusturViewModel model = new();
            model.BirimlerListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
            model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokKartiOlustur(StokKartiOlusturViewModel model)
        {
            StokKartiOlusturValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.BirimlerListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
                return View(model);
            }

            model.StokAdi = model.StokAdi.ToUpper();
            model.StokBarkod = model.StokBarkod.ToUpper();
            model.AktifMi = true;

            if (string.IsNullOrEmpty(model.Aciklama))
                model.Aciklama = "";
            else
                model.Aciklama = model.Aciklama.ToUpper();

            await _veterinerDbContext.Stoklar.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["StokKartiOlusturuldu"] = $"{model.StokAdi} isimli stok kartı başarı ile oluşturuldu.";
            return RedirectToAction();

        }

        [HttpGet]
        public IActionResult StokGoruntule()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> StokGoruntuleData()
        {
            StokGoruntuleViewModel model = new();
            model.StokListesi = await model.StokListesiniGetirAsync(_veterinerDbContext);
            return Json(model.StokListesi);
        }
        [HttpPost]
        public async Task<IActionResult> StokDetay(string secilenStokId)
        {
            if (string.IsNullOrEmpty(secilenStokId))
                return View("BadRequest");

            if (!int.TryParse(secilenStokId, out int stokId))
                return View("BadRequest");

            if (!await _veterinerDbContext.Stoklar.AnyAsync(s => s.Id == stokId))
                return View("BadRequest");

            ViewBag.StokId = secilenStokId;
            return View();
        }
        [HttpPost]
        public IActionResult StokDetayData(string secilenStokId)
        {
            if (!int.TryParse(secilenStokId, out int stokId))
                return View();
            StokDetayViewModel model = new(_veterinerDbContext, stokId);

            return Json(model);
        }

        [HttpGet]
        public IActionResult StokDuzenle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StokDuzenle(StokDuzenleStokSecViewModel model)
        {
            StokDuzenleStokSecValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(model);
            }

            StokDuzenleKaydetViewModel stokDetay = new();
            stokDetay = await stokDetay.ModeliOlusturAsync(_veterinerDbContext, model);
            stokDetay.Signature = Signature.CreateSignature(stokDetay.Id.ToString(), stokDetay.Id.ToString());

            ViewBag.StokModel = stokDetay;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokDuzenleKaydet(StokDuzenleKaydetViewModel model)
        {
            if (!Signature.VerifySignature(model.Id.ToString(), model.Id.ToString(), model.Signature))
            {
                return View("BadRequest");
            }


            StokDuzenleKaydetValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var Error in result.Errors)
                {
                    ModelState.AddModelError("", Error.ErrorMessage);
                }
                model.BirimListesi = await model.BirimListesiniGetirAsync(_veterinerDbContext);
                model.KategoriListesi = await model.KategoriListesiniGetirAsync(_veterinerDbContext);
                ViewBag.StokModel = model;
                return View("StokDuzenle");

            }

            model.StokAdi = model.StokAdi.ToUpper();
            if (model.Aciklama != null)
                model.Aciklama = model.Aciklama.ToUpper();
            model.StokBarkod = model.StokBarkod.ToUpper();

            _veterinerDbContext.Update(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["StokDuzenlendi"] = $"{model.StokAdi} isimli stoğa ait bilgiler başarı ile düzenlendi";

            return View("StokDuzenle");
        }

        [HttpGet]
        public IActionResult StokGiris()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StokGiris(StokGirisViewModel model)
        {
            StokGirisValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }
            StokGirisKaydetViewModel kaydetViewModel = new();

            var (isSuccess, aramaSonucu) = await kaydetViewModel.AramaSonucunuGetirAsync(model, _veterinerDbContext);
            if (!isSuccess)
            {
                ModelState.AddModelError("StokId", "Aradığınız stoğa ait bir kayıt bulunamadı");
                return View("StokGiris", model);
            }
            TempData["ArananMetin"] = model.ArananMetin;

            ViewBag.AramaSonucu = aramaSonucu;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokGirisKaydet(StokGirisKaydetViewModel model)
        {
            StokGirisKaydetValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);

                }
                string arananMetin = TempData["ArananMetin"].ToString();
                StokGirisViewModel girisModel = new();
                girisModel.ArananMetin = arananMetin;
                TempData["ArananMetin"] = girisModel.ArananMetin;

                var (isSuccess, aramaSonucu) = await model.AramaSonucunuGetirAsync(girisModel, _veterinerDbContext);

                ViewBag.AramaSonucu = aramaSonucu;

                return View("StokGiris", girisModel);

            }

            var stok = await model.StoguGetirAsync(model, _veterinerDbContext);
            int sayac = 0;

            foreach (var imza in model.ImzaListesi)
            {
                string string1 = stok.Id.ToString();
                string string2 = stok.StokBarkod;
                if (Signature.VerifySignature(string1, string2, imza))
                    sayac++;
            }
            if (sayac != 1)
                return View("BadRequest");

            var user = await _userManager.GetUserAsync(User);
            var stokHareket = model.StokHareketBigileriniGetir(model, user);

            _veterinerDbContext.StokHareketler.Add(stokHareket);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["StokGirisiYapildi"] = "Stok girişi başarılı bir şekilde yapıldı.";

            return View("StokGiris");
        }

        [HttpGet]
        public IActionResult StokCikis()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StokCikis(StokCikisViewModel model)
        {
            StokCikisValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }
            StokCikisKaydetViewModel kaydetViewModel = new();

            var (isSuccess, aramaSonucu) = await kaydetViewModel.AramaSonucunuGetirAsync(model, _veterinerDbContext);
            if (!isSuccess)
            {
                ModelState.AddModelError("StokId", "Aradığınız stoğa ait bir kayıt bulunamadı");
                return View("StokCikis", model);
            }
            TempData["ArananMetin"] = model.ArananMetin;

            ViewBag.AramaSonucu = aramaSonucu;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> StokCikisKaydet(StokCikisKaydetViewModel model)
        {
            StokCikisKaydetValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                string arananMetin = TempData["ArananMetin"].ToString();
                StokCikisViewModel cikisModel = new();
                cikisModel.ArananMetin = arananMetin;
                TempData["ArananMetin"] = cikisModel.ArananMetin;

                var (isSuccess, aramaSonucu) = await model.AramaSonucunuGetirAsync(cikisModel, _veterinerDbContext);

                ViewBag.AramaSonucu = aramaSonucu;

                return View("StokCikis", cikisModel);
            }

            var stok = await model.StoguGetirAsync(model, _veterinerDbContext);

            int sayac = 0;

            foreach (var imza in model.ImzaListesi)
            {
                string string1 = stok.Id.ToString();
                string string2 = stok.StokBarkod;
                if (Signature.VerifySignature(string1, string2, imza))
                    sayac++;
            }
            if (sayac != 1)
                return View("BadRequest");

            var user = await _userManager.GetUserAsync(User);
            var stokHareket = model.StokHareketBigileriniGetir(model, user);

            _veterinerDbContext.StokHareketler.Add(stokHareket);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["StokCikisiYapildi"] = "Stok çıkışı başarılı bir şekilde yapıldı.";

            return View("StokCikis");
        }

        [HttpGet]
        public IActionResult HastalikTanimla()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> HastalikTanimla(HastalikTanimlaViewModel model)
        {

            HastalikTanimlaValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(model);
            }
            model.HastalikAdi = model.HastalikAdi.ToUpper();
            _veterinerDbContext.Hastaliklar.Add(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["HastalikEklendi"] = $"{model.HastalikAdi.ToUpper()} isimli hastalik sisteme kaydedildi.";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> HastalikSil()
        {
            HastalikSilViewModel model = new();
            model.HastalikListesi = await model.HastalikListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HastalikSil(HastalikSilViewModel model)
        {
            HastalikSilValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                model.HastalikListesi = await model.HastalikListesiniGetirAsync(_veterinerDbContext);
                return View(model);
            }

            var hastalik = await model.SilinecekHastaligiGetirAsnyc(_veterinerDbContext, model);
            _veterinerDbContext.Hastaliklar.Remove(hastalik);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["HastalikSilindi"] = $"{hastalik.HastalikAdi} isimli hastalik başarı ile silindi.";

            model.HastalikListesi = await model.HastalikListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }


        [HttpGet]
        public IActionResult KanTahliliTanimla()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KanTahliliTanimla(KanTahliliTanimlaViewModel model)
        {
            KanTahliliTanimlaValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }
            model.AktifMi = true;
            model.KanTestiAdi = model.KanTestiAdi.ToUpper();
            await _veterinerDbContext.KanDegerleri.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();
            TempData["TahlilEklendi"] = $"{model.KanTestiAdi} isimli kan tahlili sisteme eklendi.";
            return RedirectToAction();
        }

        [HttpGet]
        async public Task<IActionResult> KanTahliliDuzenle()
        {
            KanTahlilleriniGetirViewModel model = new();
            model.KanTahlilleriListesi = await model.KanTahlilleriListesiniGetirAsnyc(_veterinerDbContext);

            return View(model);
        }

        [HttpPost]
        async public Task<IActionResult> KanTahliliDuzenle(KanTahlilleriniGetirViewModel model)
        {
            SecilenKanTahlilDetayiValidators validator = new();
            ValidationResult result = validator.Validate(model);
            model.KanTahlilleriListesi = await model.KanTahlilleriListesiniGetirAsnyc(_veterinerDbContext);
            if (!result.IsValid)
            {
                foreach (var erros in result.Errors)
                {
                    ModelState.AddModelError("", erros.ErrorMessage);
                }

                return View(model);
            }
            model = await model.SecilenKanTahlilDetayınıGetirAsync(model, _veterinerDbContext);

            ViewBag.SecilenKanTahlili = model;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KanTahliliDuzenleKaydet(KanTahlilleriniGetirViewModel model)
        {
            if (!Signature.VerifySignature(model.KanDegerleriId.ToString(), model.KanDegerleriId.ToString(), model.Imza))
                return View("BadRequest");
            KanTahliliDuzenleKaydetValidators validator = new();
            ValidationResult result = validator.Validate(model);

            model.KanTahlilleriListesi = await model.KanTahlilleriListesiniGetirAsnyc(_veterinerDbContext);

            //TODO validasyon hata mesajları default view e gitmiyor
            if (!result.IsValid)
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }


                ViewBag.SecilenKanTahlili = model;
                return View("KanTahliliDuzenle", model);
            }

            var duzenlenmisKanTahlili = await model.KanTahlilBilgileriniDuzenle(model, _veterinerDbContext);

            _veterinerDbContext.Update(duzenlenmisKanTahlili);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["TahlilDuzenlendi"] = "Seçilen Kan Tahlili Başarı İle Düzenlendi.";


            return View("KanTahliliDuzenle", model);

        }

        [HttpGet]
        public async Task<IActionResult> KanTahliliSil()
        {
            KanTahlilleriniGetirViewModel model = new();
            model.KanTahlilleriListesi = await model.KanTahlilleriListesiniGetirAsnyc(_veterinerDbContext);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KanTahliliSil(KanTahlilleriniGetirViewModel model)
        {
            KanTahliliSilValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }

            var silinecekKanTahlili = await model.SilinecekKanTahliliniGetirAsync(model, _veterinerDbContext);
            _veterinerDbContext.Remove(silinecekKanTahlili);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["KanTahliliSilindi"] = $"{silinecekKanTahlili.KanTestiAdi.ToUpper()} isimli kan tahlili sistemden silindi";
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult FotografEkle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FotografEkle(AnaSayfaFotografEkleViewModel model)
        {
            AnaSayfaFotograflarValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                    return View(model);
                }
            }


            var dosyaUzantısı = Path.GetExtension(model.FilePhoto.FileName);
            var dosyaAdi = $"{model.FotografAdi.ToLower()}{dosyaUzantısı}";
            var AnaSayfaFotografKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\anaSayfaFotograf");

            if (!Directory.Exists(AnaSayfaFotografKlasoru))
            {
                Directory.CreateDirectory(AnaSayfaFotografKlasoru);
            }

            var filePath = Path.Combine(AnaSayfaFotografKlasoru, dosyaAdi);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.FilePhoto.CopyToAsync(stream);
            }

            // Web URL'sini oluşturma
            var fileUrl = $"/img/anaSayfaFotograf/{dosyaAdi}";

            // Veritabanına URL'yi kaydetme
            model.Url = fileUrl;
            model.AktifMi = false;


            await _veterinerDbContext.AnaSayfaFotograflar.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["Kaydedildi"] = $"{model.FotografAdi.ToUpper()} fotoğrafı kaydedildi";

            return RedirectToAction();
        }


        [HttpGet]
        public async Task<IActionResult> FotografDuzenle()
        {
            FotografDuzenleViewModel model = new();
            model.FotograflarListesi = await model.FotograflariGetirAsync(_veterinerDbContext);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FotografSil(string Imza, string AnaSayfaFotograflarId)
        {
            if (!Signature.VerifySignature(AnaSayfaFotograflarId, AnaSayfaFotograflarId, Imza))
                return View("BadRequest");

            var fotograf = await _veterinerDbContext.AnaSayfaFotograflar.FindAsync(Convert.ToInt32(AnaSayfaFotograflarId));

            var silinecekFotograf = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fotograf.Url.TrimStart('/'));

            System.IO.File.Delete(silinecekFotograf);

            _veterinerDbContext.AnaSayfaFotograflar.Remove(fotograf);
            await _veterinerDbContext.SaveChangesAsync();

            return RedirectToAction("FotografDuzenle");
        }

        [HttpPost]
        public async Task<IActionResult> FotografSec(string Imza, string AnaSayfaFotograflarId)
        {
            if (!Signature.VerifySignature(AnaSayfaFotograflarId, AnaSayfaFotograflarId, Imza))
                return View("BadRequest");

            var fotograf = await _veterinerDbContext.AnaSayfaFotograflar.FindAsync(Convert.ToInt32(AnaSayfaFotograflarId));

            if (fotograf.AktifMi)
                fotograf.AktifMi = false;
            else
                fotograf.AktifMi = true;

            _veterinerDbContext.AnaSayfaFotograflar.Update(fotograf);
            await _veterinerDbContext.SaveChangesAsync();

            return RedirectToAction("FotografDuzenle");
        }


        [HttpGet]
        public IActionResult HakkimizdaIcerikOlustur()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HakkimizdaIcerikOlustur(HakkimizdaIcerikOlusturViewModel model)
        {
            HakkimizdaIcerikOlusturValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                    return View(model);
                }
            }

            model.AktifMi = false;
            await _veterinerDbContext.Hakkimizda.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["HakkimizdaIcerikKaydedildi"] = "İçerik başarı ile kaydedildi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> HakkimizdaIcerikDuzenle()
        {
            HakkimizdaIcerikDuzenleViewModel model = new();
            model.Icerikler = await model.IcerikleriGetirAsync(_veterinerDbContext);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> HakkimizdaIcerikSil(string Imza, string HakkimizdaId)
        {
            if (!Signature.VerifySignature(HakkimizdaId, HakkimizdaId, Imza))
                return View("BadRequest");

            var id = Convert.ToInt32(HakkimizdaId);
            var hakkimizdaIcerik = await _veterinerDbContext.Hakkimizda.FindAsync(id);
            _veterinerDbContext.Hakkimizda.Remove(hakkimizdaIcerik);
            await _veterinerDbContext.SaveChangesAsync();

            return RedirectToAction("HakkimizdaIcerikDuzenle");
        }

        [HttpPost]
        public async Task<IActionResult> HakkimizdaIcerikSec(string Imza, string HakkimizdaId)
        {
            if (!Signature.VerifySignature(HakkimizdaId, HakkimizdaId, Imza))
                return View("BadRequest");

            var id = Convert.ToInt32(HakkimizdaId);
            var hakkimizdaIcerik = await _veterinerDbContext.Hakkimizda.FindAsync(id);
            if (hakkimizdaIcerik.AktifMi)
                hakkimizdaIcerik.AktifMi = false;
            else
                hakkimizdaIcerik.AktifMi = true;

            _veterinerDbContext.Hakkimizda.Update(hakkimizdaIcerik);
            await _veterinerDbContext.SaveChangesAsync();

            return RedirectToAction("HakkimizdaIcerikDuzenle");
        }

        [HttpGet]
        public async Task<IActionResult> HakkimizdaIcerikEditle(string Imza, string HakkimizdaId)
        {
            if (!Signature.VerifySignature(HakkimizdaId, HakkimizdaId, Imza))
                return View("BadRequest");

            var id = Convert.ToInt32(HakkimizdaId);
            var hakkimizdaIcerik = await _veterinerDbContext.Hakkimizda.FindAsync(id);

            HakkimizdaIcerikEditleViewModel model = new();
            model.HakkimizdaId = hakkimizdaIcerik.HakkimizdaId;
            model.Baslik = hakkimizdaIcerik.Baslik;
            model.Aciklama = hakkimizdaIcerik.Aciklama;
            model.Imza = Signature.CreateSignature(hakkimizdaIcerik.HakkimizdaId.ToString(), hakkimizdaIcerik.HakkimizdaId.ToString());
            model.AktifMi = hakkimizdaIcerik.AktifMi;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HakkimizdaIcerikEditle(HakkimizdaIcerikEditleViewModel model)
        {
            if (!Signature.VerifySignature(model.HakkimizdaId.ToString(), model.HakkimizdaId.ToString(), model.Imza))
                return View("BadRequest");

            HakkimizdaIcerikEditleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }

                return View(model);
            }

            var icerik = await _veterinerDbContext.Hakkimizda.FindAsync(model.HakkimizdaId);

            icerik.Baslik = model.Baslik;
            icerik.Aciklama = model.Aciklama;

            _veterinerDbContext.Update(icerik);
            await _veterinerDbContext.SaveChangesAsync();

            return RedirectToAction("HakkimizdaIcerikDuzenle");
        }

        [HttpGet]
        public IActionResult AdresEkle()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdresEkle(AdresEkleViewModel model)
        {
            AdresEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(model);
            }

            model.AktifMi = false;

            await _veterinerDbContext.IletisimBilgileri.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["AdresEklendi"] = $"{model.SubeAdi} şubesi için adres bilgisi eklendi.";

            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> AdresDuzenle()
        {
            AdresDuzenleViewModel model = new();
            model.IletisimBilgileriListesi = await model.IletisimBilgileriListesiniGetirAsync(_veterinerDbContext);
            return View(model);
        }

        [HttpGet]
        public IActionResult SosyalMedyaEkle()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SosyalMedyaEkle(SosyalMedyaEkleViewModel model)
        {
            SosyalMedyaEkleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            if (!model.SosyalMedyaUrl.Contains("https://"))
                model.SosyalMedyaUrl = "https://" + model.SosyalMedyaUrl;

            await _veterinerDbContext.SosyalMedyalar.AddAsync(model);
            await _veterinerDbContext.SaveChangesAsync();

            TempData["SosyalMedyaEklendi"] = $"{model.SosyalMedyaAdi.ToUpper()} için bilgiler kaydedildi.";
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> SosyalMedyaSec(string IletisimBilgileriId, string Imza, List<string> SecilenSosyalMedyaIdler)
        {
            if (!Signature.VerifySignature(IletisimBilgileriId, IletisimBilgileriId, Imza))
                return View("BadRequest");

            int iletisimBilgileriId;
            int secilenSosyalMedyaId;
            List<int> secilenSosyalMedyaIdListesi = new();

            foreach (var item in SecilenSosyalMedyaIdler)
            {
                if (!int.TryParse(item, out secilenSosyalMedyaId))
                    return View("BadRequest");
                secilenSosyalMedyaIdListesi.Add(secilenSosyalMedyaId);

            }
            if (!int.TryParse(IletisimBilgileriId, out iletisimBilgileriId))
                return View("BadRequest");

            List<SosyalMedya> SecilenSosyalMedyalar = await _veterinerDbContext.SosyalMedyalar
                                                                                .Where(sm => secilenSosyalMedyaIdListesi.Contains(sm.SosyalMedyaId))
                                                                                .ToListAsync();

            List<IletisimBilgileriSosyalMedya> updateEdilecekIletisimBilgileri = await _veterinerDbContext.IletisimBilgileriSosyalMedyalar
                                                                                        .Where(ibsm => ibsm.IletisimBilgileriId == iletisimBilgileriId)
                                                                                        .ToListAsync();

            List<SosyalMedya> SecilmeyenSosyalMedyalar = _veterinerDbContext.SosyalMedyalar
                .AsEnumerable()
                .Except(SecilenSosyalMedyalar)
                .ToList();

            foreach (var sosyalMedya in SecilenSosyalMedyalar)
            {
                if (!updateEdilecekIletisimBilgileri.Any(ibsm => ibsm.SosyalMedyaId == sosyalMedya.SosyalMedyaId))
                {
                    _veterinerDbContext.IletisimBilgileriSosyalMedyalar.Add(new IletisimBilgileriSosyalMedya
                    {
                        IletisimBilgileriId = iletisimBilgileriId,
                        SosyalMedyaId = sosyalMedya.SosyalMedyaId
                    });
                }
            }

            var silinecekIletisimBilgileri = updateEdilecekIletisimBilgileri
                .Where(ibsm => !SecilenSosyalMedyalar.Any(sm => sm.SosyalMedyaId == ibsm.SosyalMedyaId))
                .ToList();

            foreach (var silinecek in silinecekIletisimBilgileri)
            {
                _veterinerDbContext.IletisimBilgileriSosyalMedyalar.Remove(silinecek);
            }

            await _veterinerDbContext.SaveChangesAsync();

            AdresDuzenleViewModel model = new();
            model.IletisimBilgileriListesi = await model.IletisimBilgileriListesiniGetirAsync(_veterinerDbContext);
            return View("AdresDuzenle", model);
        }

        [HttpPost]
        public async Task<IActionResult> AdresSec(AdresDuzenleViewModel model)
        {

            if (!Signature.VerifySignature(model.IletisimBilgileriId.ToString(), model.IletisimBilgileriId.ToString(), model.Imza))
                return View("BadRequest");
            var iletisimBigisi = await _veterinerDbContext.IletisimBilgileri.FindAsync(model.IletisimBilgileriId);

            if (iletisimBigisi.AktifMi)
                iletisimBigisi.AktifMi = false;
            else
                iletisimBigisi.AktifMi = true;

            _veterinerDbContext.IletisimBilgileri.Update(iletisimBigisi);
            await _veterinerDbContext.SaveChangesAsync();

            model.IletisimBilgileriListesi = await model.IletisimBilgileriListesiniGetirAsync(_veterinerDbContext);
            return View("AdresDuzenle", model);
        }

        [HttpPost]
        public async Task<IActionResult> AdresSil(AdresDuzenleViewModel model)
        {

            if (!Signature.VerifySignature(model.IletisimBilgileriId.ToString(), model.IletisimBilgileriId.ToString(), model.Imza))
                return View("BadRequest");

            IletisimBilgileri iletisimBigisi = await _veterinerDbContext.IletisimBilgileri.FindAsync(model.IletisimBilgileriId);
            List<IletisimBilgileriSosyalMedya> iletisimBilgisiSosyalMedyaListesi = await _veterinerDbContext.IletisimBilgileriSosyalMedyalar
                                                                        .Where(ibsm => ibsm.IletisimBilgileriId == model.IletisimBilgileriId)
                                                                        .ToListAsync();

            _veterinerDbContext.RemoveRange(iletisimBilgisiSosyalMedyaListesi);
            await _veterinerDbContext.SaveChangesAsync();
            _veterinerDbContext.IletisimBilgileri.Remove(iletisimBigisi);
            await _veterinerDbContext.SaveChangesAsync();


            model.IletisimBilgileriListesi = await model.IletisimBilgileriListesiniGetirAsync(_veterinerDbContext);
            return View("AdresDuzenle", model);
        }

        [HttpGet]
        public async Task<IActionResult> AdresEditle(AdresDuzenleViewModel model)
        {

            if (!Signature.VerifySignature(model.IletisimBilgileriId.ToString(), model.IletisimBilgileriId.ToString(), model.Imza))
                return View("BadRequest");
            var iletisimBilgileri = await _veterinerDbContext.IletisimBilgileri.FindAsync(model.IletisimBilgileriId);

            AdresEditleViewModel returnModel = new();
            returnModel.IletisimBilgileriId = iletisimBilgileri.IletisimBilgileriId;
            returnModel.SubeAdi = iletisimBilgileri.SubeAdi;
            returnModel.Sehir = iletisimBilgileri.Sehir;
            returnModel.Ilce = iletisimBilgileri.Ilce;
            returnModel.Mahalle = iletisimBilgileri.Mahalle;
            returnModel.Cadde = iletisimBilgileri.Cadde;
            returnModel.Sokak = iletisimBilgileri.Sokak;
            returnModel.No = iletisimBilgileri.No;
            returnModel.TelefonNumarasi = iletisimBilgileri.TelefonNumarasi;
            returnModel.Imza = Signature.CreateSignature(iletisimBilgileri.IletisimBilgileriId.ToString(), iletisimBilgileri.IletisimBilgileriId.ToString());
            returnModel.AktifMi = iletisimBilgileri.AktifMi;

            return View(returnModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdresEditle(AdresEditleViewModel model)
        {

            if (!Signature.VerifySignature(model.IletisimBilgileriId.ToString(), model.IletisimBilgileriId.ToString(), model.Imza))
                return View("BadRequest");

            AdresEditleValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(model);
            }

            _veterinerDbContext.IletisimBilgileri.Update(model);
            await _veterinerDbContext.SaveChangesAsync();


            AdresDuzenleViewModel returnModel = new();
            returnModel.IletisimBilgileriListesi = await returnModel.IletisimBilgileriListesiniGetirAsync(_veterinerDbContext);
            return View("AdresDuzenle", returnModel);
        }
    }
}

