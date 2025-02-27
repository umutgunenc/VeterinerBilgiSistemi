﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.Account;
using VeterinerBilgiSistemi.Models.ViewModel.Account;
using System;
using VeterinerBilgiSistemi.Fonksiyonlar;
using System.IO;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using FaceRecognitionDotNet;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly VeterinerDBContext _context;
        private readonly IEmailSender _emailSender;
        private readonly FaceRecognition _faceRecognition;
        private const string ModelDirectory = "wwwroot/models";
        private const string ShapePredictorPath = "wwwroot/models/shape_predictor_68_face_landmarks.dat";
        private const string FaceRecognitionModelPath = "wwwroot/models/dlib_face_recognition_resnet_model_v1.dat";


        public AccountController(UserManager<AppUser> userManager, VeterinerDBContext context, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _faceRecognition = FaceRecognition.Create(ModelDirectory);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.rolId = _context.Roles
                .Where(x => x.Name == "MÜŞTERI")
                .Select(r => r.Id)
                .FirstOrDefault();

            model.InsanAdi = model.InsanAdi.ToUpper();
            model.InsanSoyadi = model.InsanSoyadi.ToUpper();
            model.Email = model.Email.ToLower();
            model.CalisiyorMu = false;
            model.SifreOlusturmaTarihi = DateTime.Now;
            model.SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
            model.UserName = model.UserName.ToUpper();

            var sifre = model.PasswordHash;

            RegisterValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }


            var createUser = await _userManager.CreateAsync(model, model.PasswordHash);

            if (!createUser.Succeeded)
            {
                foreach (var error in createUser.Errors)
                {
                    ModelState.AddModelError("PasswordHash", error.Description);
                }
                return View(model);
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var userKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", user.Id.ToString());

                if (!Directory.Exists(userKlasoru))
                {
                    Directory.CreateDirectory(userKlasoru);
                }

                var filePath = Path.Combine(userKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/user/{user.Id}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                user.ImgURL = fileUrl;
                await _context.SaveChangesAsync();
            }

            IdentityUserRole<int> userRole = new()
            {
                RoleId = model.rolId,
                UserId = model.Id
            };

            await _context.UserRoles.AddAsync(userRole);


            if (await _context.SaveChangesAsync() > 0)
            {
                var loginUrl = Url.Action("Login", "Account", null, Request.Scheme);
                var kullaniciAdSoyad = model.InsanAdi.ToUpper() + " " + model.InsanSoyadi.ToUpper();
                var kullaniciAdi = model.UserName;
                var tarih = DateTime.Now.ToString("HH:mm dd/MM/yyyy");

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
                            <p>Sayın {kullaniciAdSoyad}, {tarih} tarihinde sisteme başarıyla üye oldunuz. Aşağıda giriş bilgileriniz yer almaktadır:</p>
                            <div class='credentials'>
                                <p><strong>Kullanıcı Adı:</strong> {kullaniciAdi}</p>
                                <p><strong>Şifre:</strong> {sifre}</p>
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
                    TempData["KisiEklendi"] = $"{model.InsanAdi.ToUpper()} {model.InsanSoyadi.ToUpper()} isimli kişi sisteme kaydedildi. Kullanıcı adı ve şifresi {model.Email.ToUpper()} adresine gönderildi.";
                }
                catch (Exception ex)
                {
                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Kayıt işlemi tamamlanamadı." + " " + ex.Message;
                    _context.Users.Remove(model);
                    _context.UserRoles.Remove(userRole);
                    await _context.SaveChangesAsync();
                    return View(model);
                }


                return View(model);


            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            LoginValidators validator = new();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }


            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");
                return View(model);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (user.CalisiyorMu == false && (roles.Contains("ÇALIŞAN") || roles.Contains("VETERİNER") || roles.Contains("ADMIN")))
            {
                ModelState.AddModelError("PasswordHash", "Kullanıcı aktif değil. Lütfen yöneticinizle iletişime geçiniz.");
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: false, lockoutOnFailure: true);

            if (signInResult.Succeeded)
            {
                if (string.IsNullOrEmpty(ReturnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(ReturnUrl);
            }




            if (signInResult.IsLockedOut)
            {
                if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.ToLocalTime() > DateTime.Now)
                    ModelState.AddModelError("LockoutEnd", $"Hesabınız kilitlenmiştir. Hesabınızın kilidi {user.LockoutEnd.Value.ToLocalTime().ToString("g")} 'de açılacaktır.");

                else
                    ModelState.AddModelError("LockoutEnd", "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");

            }
            if (user.AccessFailedCount >= 0)
            {
                ModelState.AddModelError("AccessFailedCount", $"Hesabınıza 3 defa yanlış giriş yaptığınızda kitlenecektir. {user.AccessFailedCount} kere yanlış giriş yapıldı.");
                ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");
            }
            ModelState.AddModelError("PasswordHash", "Kullanıcı adı veya şifre hatalı.");

            return View(model);



        }


        public IActionResult LoginWithFaceId()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithFaceId(IFormFile[] filePhotos)
        {
            if (filePhotos == null || filePhotos.Length <= 10)
                return Json(new { success = false, message = "Fotoğraf çekme işlemi başarısız oldu" });

            try
            {
                FaceRecognitionClass faceRecognitionClass = new();

                var result = await faceRecognitionClass.DetectFacesAsync(filePhotos);

                if (result.Item2 == false)
                    return Json(new { success = false, message = "Yüz tespit işlemi başarısız oldu." });

                var copareResault = await faceRecognitionClass.CompareFaces(result.Item1, _context);

                if (!copareResault.Item1)
                    return Json(new { success = false, message = "Sistemde kayıtlı yüz bilgisi bulunamadı." });

                if (!await _signInManager.CanSignInAsync(copareResault.Item2))
                    return Json(new { success = false, message = "Login işlemi başarısız oldu" });

                await _signInManager.SignInAsync(copareResault.Item2,false);
                return Json(new { success = true, message = "Başarıyla giriş yaptınız, ana sayfaya yönlendiriliyorsunuz. Lütfen bekleyin..." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }







        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ForgotPasswordValidators validator = new();
            ValidationResult result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.InsanTckn == model.InsanTckn && x.Email == model.Email && x.PhoneNumber == model.PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Girdiğiniz bilgiler birbirleri ile uyuşmuyor. Girdiğiniz TCKN, email adresi veya telefon numarası sistemde kayıtlı değil.");
                return View(model);
            }
            else
            {
                var yeniSifre = SifreOlustur.GeneratePassword(8);

                // Yeni şifreyi hashle
                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, yeniSifre);

                // Eski şifreyi sakla
                var eskiSifreHash = user.PasswordHash;

                // Hashlenmiş şifreyi kullanıcıya ata
                user.PasswordHash = hashedNewPassword;

                // Kullanıcıyı güncelle
                var EskiSifreOlusturmaTarihi = user.SifreOlusturmaTarihi;
                var EskiSifreGecerlilikTarihi = user.SifreGecerlilikTarihi;
                user.SifreOlusturmaTarihi = DateTime.Now;
                user.SifreGecerlilikTarihi = DateTime.Now.AddDays(120);
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("Email", error.Description);
                    }
                    return View(model);
                }

                var loginUrl = Url.Action("Login", "Account", null, Request.Scheme);
                var kullaniciAdSoyad = user.InsanAdi.ToUpper() + " " + user.InsanSoyadi.ToUpper();
                var kullaniciAdi = user.UserName;
                var tarih = DateTime.Now.ToString("HH:mm dd/MM/yyyy");


                string mailMessage = $@"
                <!DOCTYPE html>
                <html lang='tr'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Şifre Yenileme</title>
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
                            <h1>Şifre Yenileme</h1>
                        </div>
                        <div class='content'>
                            <p>Sayın {kullaniciAdSoyad},</p>
                            <p>Şifre sıfırlama talebiniz {tarih} tarihinde başarıyla gerçekleştirilmiştir. Aşağıda yeni giriş bilgileriniz yer almaktadır:</p>
                            <div class='credentials'>
                                <p><strong>Kullanıcı Adı:</strong> {kullaniciAdi}</p>
                                <p><strong>Yeni Şifre:</strong> {yeniSifre}</p>
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
                    await _emailSender.SendEmailAsync(user.Email, "Şifre Yenileme Talebi", mailMessage);
                    TempData["SifreGonderildi"] = $"{kullaniciAdSoyad} isimli kişinin şifresi {user.Email.ToUpper()} adresine gönderildi.";
                    return View();

                }
                catch (Exception ex)
                {
                    user.PasswordHash = eskiSifreHash;
                    user.SifreOlusturmaTarihi = EskiSifreOlusturmaTarihi;
                    user.SifreGecerlilikTarihi = EskiSifreGecerlilikTarihi;
                    await _userManager.UpdateAsync(user);

                    ViewBag.Hata = "Mail Gönderme işlemi başarısız oldu. Şifre gönderme işlemi tamamlanamadı." + " " + ex.Message;
                    return View(model);
                }


            }
        }
    }
}
