using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Enum;
using VeterinerBilgiSistemi.Models.Validators.Animal;
using VeterinerBilgiSistemi.Models.ViewModel.Animal;

//TODO hayvan ekle kısmında profil fotografı için seç seçeneğin ekle, diğerlerini sil, eklemez ise default foto gelsin
//TODO kişi ekle kısmında profil fotografı için seç seçeneğin ekle, diğerlerini sil, eklemez ise default foto gelsin

namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerDBContext _context;
        private readonly IEmailSender _emailSender;
        public AnimalController(UserManager<AppUser> usermanager, VeterinerDBContext context, IEmailSender emailSender)
        {
            _userManager = usermanager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> AddAnimal()
        {
            AddAnimalViewModel model = new();

            await model.ModeliOlusturAsync(_context);

            return View(model);

        }

        public async Task<JsonResult> TurleriGetir(int cinsId)
        {
            var turler = await _context.CinsTur
                .Where(ct => ct.CinsId == cinsId)
                .Join(_context.Turler,
                      ct => ct.TurId,
                      t => t.TurId,
                      (ct, t) => new SelectListItem
                      {
                          Text = t.TurAdi,
                          Value = t.TurId.ToString()
                      }).ToListAsync();

            return Json(turler);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalViewModel model)
        {

            var validator = new HayvanEkleValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model = await model.GeriDonusModeliOlusturAsync(_context);

                return View(model);

            };

            var hayvan = await model.KaydedilecekHayvaniOlusturAsync(_context);
            //hayvanın id degerine ulasabilmek için öncelikle onu db'ye kaydetmek gerekiyor.
            await _context.Hayvanlar.AddAsync(hayvan);
            await _context.SaveChangesAsync();


            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }

                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                hayvan.ImgUrl = fileUrl;
            }
            else
            {
                hayvan.ImgUrl = null;
            }

            _context.Hayvanlar.Update(hayvan);
            await _context.SaveChangesAsync();

            var user = await _userManager.GetUserAsync(User);

            SahipHayvan sahipHayvan = new SahipHayvan()
            {
                SahipId = user.Id,
                SahiplenmeTarihi = model.SahiplenmeTarihi,
                HayvanId = hayvan.HayvanId,
                AktifMi = true

            };

            await _context.SahipHayvan.AddAsync(sahipHayvan);
            await _context.SaveChangesAsync();

            TempData["AddAnimal"] = $"{model.HayvanAdi} isimli hayvan başarıyla eklendi.";

            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> Information(int hayvanId)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var kullanici = await _userManager.GetUserAsync(User);

            var kullaniciHayvanlari = await _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.SahipId == kullanici.Id && sh.AktifMi == true)
                .FirstOrDefaultAsync();

            if (kullaniciHayvanlari == null)
            {
                return View("BadRequest");
            }

            HayvanlarBilgiViewModel model = new();
            model = await model.HayvanBilgileriniGetirAsync(hayvan, kullanici, _context);

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> EditAnimal(int hayvanId)
        {

            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var user = await _userManager.GetUserAsync(User);

            var userHayvan = await _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.AppUser.InsanTckn == user.InsanTckn)
                .FirstOrDefaultAsync();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }
            EditAnimalViewModel model = new();
            model = await model.ModelOlusturAsync(hayvan, user, _context);
            return View(model);

        }

        //TODO kontrol et
        [HttpPost]
        public async Task<IActionResult> EditAnimal(EditAnimalViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!Signature.VerifySignature(model.HayvanId.ToString(), model.SahipTckn, model.Imza))
            {
                return View("Badrequest");
            }

            EditHayvanValidator validator = new EditHayvanValidator();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                EditAnimalViewModel returnModel = new();
                returnModel = await returnModel.ModelOlusturAsync(model, user, _context);

                return View("EditAnimal", returnModel);
            }


            var hayvan = await model.HayvaniGetirAsync(_context);

            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(hayvanKlasoru);
                if (eskiFotograflar.Length > 0)
                {
                    foreach (var eskiFotograf in eskiFotograflar)
                    {
                        System.IO.File.Delete(eskiFotograf);
                    }
                }

                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                hayvan.ImgUrl = fileUrl;

            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
                hayvan.ImgUrl = (await _context.Hayvanlar.FindAsync(model.HayvanId)).ImgUrl;
            else if (model.PhotoOption == "deletePhoto")
                hayvan.ImgUrl = null;
            else if (model.PhotoOption == "keepPhoto")
                hayvan.ImgUrl = (await _context.Hayvanlar.FindAsync(model.HayvanId)).ImgUrl;



            if (model.SahiplikCikisTarihi != null)
            {
                var sahipHayvan = await model.SahipHayvanGetirAsync(user, _context, hayvan);
                sahipHayvan.AktifMi = false;
                _context.SahipHayvan.Update(sahipHayvan);
                await _context.SaveChangesAsync();

                return RedirectToAction("Information", "User");
            }

            var guncelHayvan = await model.GuncelHayvanBilgileriniGetirAsnyc(_context, hayvan);

            _context.Hayvanlar.Update(guncelHayvan);
            await _context.SaveChangesAsync();
            TempData["Edit"] = "Hayvan bilgileri başarıyla güncellendi.";

            EditAnimalViewModel editedModel = new();
            editedModel = await editedModel.ModelOlusturAsync(hayvan, user, _context);

            return View("EditAnimal", editedModel);

        }

        [HttpGet]
        public async Task<IActionResult> AddSahip(int hayvanId)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var user = await _userManager.GetUserAsync(User);

            var userHayvan = await _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.AppUser.InsanTckn == user.InsanTckn)
                .FirstOrDefaultAsync();

            if (userHayvan == null)
                return View("BadRequest");

            AddNewSahipViewModel ViewModel = new();
            ViewModel = await ViewModel.ViewModelOlusturAsync(hayvan, user, _context);

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSahip(AddNewSahipViewModel model)
        {

            if (!Signature.VerifySignature(model.HayvanId.ToString(), model.UserTCKN, model.Signature))
                return View("Badrequest");


            AddYeniSahipValidator validator = new AddYeniSahipValidator();
            ValidationResult result = validator.Validate(model);

            var hayvan = await _context.Hayvanlar.FindAsync(model.HayvanId);

            AddNewSahipViewModel returnModel = new();
            returnModel = await returnModel.ViewModelOlusturAsync(hayvan, _userManager.GetUserAsync(User).Result, _context);
            returnModel.YeniSahipTCKN = model.YeniSahipTCKN;

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                    return View(returnModel);
                }
            }

            var yeniSahip = await _context.Users
                .Where(u => u.InsanTckn == model.YeniSahipTCKN)
                .FirstOrDefaultAsync();

            var acceptUrl = Url.Action("EmailConfirmYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.YeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId.ToString(), model.YeniSahipTCKN) }, Request.Scheme, Request.Host.Value);
            var declineUrl = Url.Action("EmailRejectYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.YeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId.ToString(), model.YeniSahipTCKN) }, Request.Scheme, Request.Host.Value);
            var cinsiyet = hayvan.HayvanCinsiyet == Cinsiyet.Erkek ? "Erkek" : "Dişi";
            var dogumTarihi = hayvan.HayvanDogumTarihi.ToString("dd-MM-yyyy");
            var olumTarihi = hayvan.HayvanOlumTarihi != null ? hayvan.HayvanOlumTarihi?.ToString("dd-MM-yyyy") : "Hayatta";
            var sahipAdSoyad = _userManager.GetUserAsync(User).Result.InsanAdi + " " + _userManager.GetUserAsync(User).Result.InsanSoyadi;
            var turAdi = await model.TurAdiniGetirAsync(_context, hayvan);
            var cinsAdi = await model.CinsAdiniGetirAsync(_context, hayvan);
            var renkAdi = await model.RenkAdiniGetirAsync(_context, hayvan);
            var hayvanAdi = hayvan.HayvanAdi;
            // Uygulamanın geçerli URL'sini almak için
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string resimYolu = hayvan.ImgUrl != null ? hayvan.ImgUrl : Url.Content("/img/animal.png");
            string imgUrl = $"{baseUrl}{resimYolu}";


            string mailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>

             <style>
                  body {{
                      font-family: Arial, sans-serif;
                      color: #333;
                      margin: 0;
                      padding: 0;
                      font-size: 1.2rem;
                      background-color: #f8f9fa;
                  }}
                  h2 {{font-size: 1.8rem;
                  }}
                  h3 {{font-size: 1.6rem;
                  }}
                  h6 {{font-size: 1.2rem;
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
                  .btn {{
                      display: inline-block;
                      padding: 10px 20px;
                      color: white !important;
                      text-decoration: none;
                      border-radius: 5px;
                      font-weight: bold;
                      text-align: center;
                      margin-top: 10px;
                      background-color: #6c757d;
                      border: none;
                  }}
                  .btn:hover {{
                      background-color: #5a6268;
                  }}
                  .btn-secondary {{
                      background-color: #6c757d;
                      margin-right: 10px;
                  }}
                  .img-container {{
                      text-align: center;
                      margin-bottom: 20px;
                  }}
                  .img-container img {{
                      border-radius: 8px;
                      max-width: 100%;
                      height: auto;
                      object-fit: cover;
                  }}
                  .details {{
                      margin-top: 20px;
                  }}
                  .details h6 {{
                      margin: 0 0 10px 0;
                  }}
                  .footer {{
                      text-align: center;
                      padding: 20px;
                      font-size: 12px;
                      color: #888;
                      border-top: 1px solid #e9ecef;
                      margin-top: 20px;
                  }}
            </style>
            </head>
            <body>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Hayvan Sahiplendirme Talebi</h2>
                    </div>
                    <div class='content'>
                        <h3>Merhaba {yeniSahip.InsanAdi} {yeniSahip.InsanSoyadi},</h3>
                        <p>{sahipAdSoyad} isimli kullanıcı size bir hayvan sahiplendirme talebi gönderdi. Aşağıda hayvana ait bilgileri bulabilirsiniz:</p>
                        
                        <div class='img-container'>
                            <img src='{imgUrl}' alt='{hayvanAdi}' />
                        </div>

                        <div class='details'>
                            <h6><strong>Hayvan Adı:</strong> {hayvanAdi}</h6>
                            <h6><strong>Türü:</strong> {turAdi}</h6>
                            <h6><strong>Cinsi:</strong> {cinsAdi}</h6>
                            <h6><strong>Rengi:</strong> {renkAdi}</h6>
                            <h6><strong>Kilosu:</strong> {hayvan.HayvanKilo} kg</h6>
                            <h6><strong>Cinsiyeti:</strong> {cinsiyet}</h6>
                            <h6><strong>Doğum Tarihi:</strong> {dogumTarihi}</h6>
                            <h6><strong>Ölüm Tarihi:</strong> {olumTarihi}</h6>
                        </div>

                        <div>
                            <a href='{acceptUrl}' class='btn btn-secondary'>Kabul Et</a>
                            <a href='{declineUrl}' class='btn btn-secondary'>Red Et</a>
                        </div>
                    </div>
                    <div class='footer'>
                        <p>Bu e-postayı aldıysanız ancak bu talebi yapmadıysanız, lütfen bizimle iletişime geçin.</p>
                    </div>
                </div>
            </body>
            </html>
            ";


            try
            {
                await _emailSender.SendEmailAsync(yeniSahip.Email, "Hayvan Sahiplenme", mailBody);
                ViewBag.Mail = "Yeni sahip ekleme talebi, kişinin mail adresine başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                ViewBag.MailHata = "Mail gönderilirken bir hata oluştu. Hata: " + ex.Message;
                return View(returnModel);
            }

            return View(returnModel);
        }

        public async Task<IActionResult> EmailConfirmYeniSahip(int hayvanId, string yeniSahipTCKN, string imza)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.InsanTckn != yeniSahipTCKN)
                return View("BadRequest");

            if (!Signature.VerifySignature(hayvanId.ToString(), yeniSahipTCKN, imza))
                return View("BadRequest");

            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var yeniSahip = await _context.Users.Where(u => u.InsanTckn == yeniSahipTCKN).FirstOrDefaultAsync();
            if (!_context.SahipHayvan.Any(x => x.HayvanId == hayvanId && x.SahipId == yeniSahip.Id))
            {
                await _context.SahipHayvan.AddAsync(new SahipHayvan
                {
                    HayvanId = hayvanId,
                    SahipId = yeniSahip.Id,
                    SahiplenmeTarihi = DateTime.Now,
                    AktifMi = true
                });
                await _context.SaveChangesAsync();
                TempData["YeniHayvanEklendi"] = $"{hayvan.HayvanAdi.ToUpper()} isimli yeni evcil hayvanınız hesabınıza başarı ile eklendi.";
            }
            else
                TempData["HayvanSahibisiniz"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanın zaten sahibisiniz.";

            return RedirectToAction("Information", "User");
        }

        public async Task<IActionResult> EmailRejectYeniSahip(int hayvanId, string yeniSahipTCKN, string imza)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.InsanTckn != yeniSahipTCKN)
                return View("BadRequest");

            if (!Signature.VerifySignature(hayvanId.ToString(), yeniSahipTCKN, imza))
                return View("BadRequest");

            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);

            TempData["EvcilHayvanRed"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanı hesabınıza eklemek istemediniz.";
            return RedirectToAction("Information", "User");

        }

        [HttpGet]
        public async Task<IActionResult> MuayeneKayitlari(MuayenelerViewModel model)
        {
            var sahip = await _userManager.GetUserAsync(User);
            if (sahip == null)
                return View("BadRequest");
            var sahibinHayvani = await _context.SahipHayvan
                .FirstOrDefaultAsync(sh => sh.SahipId == sahip.Id && sh.HayvanId == model.HayvanId);
            if (sahibinHayvani == null)
                return View("BadRequest");

            model.MuayenelerListesi = await model.MuayeneKayitlariniGetirAsync(_context);
            model.SonSayfaNumarasi = await model.ToplamSayfaSayisiniGetirAsync(_context);


            return View(model);
        }
    }
}
