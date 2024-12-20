﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.Account;
using VeterinerBilgiSistemi.Models.Validators.User;
using VeterinerBilgiSistemi.Models.ViewModel.Account;
using VeterinerBilgiSistemi.Models.ViewModel.Animal;
using VeterinerBilgiSistemi.Models.ViewModel.User;
using FaceRecognitionDotNet;
using System.Drawing;
using VeterinerBilgiSistemi.Fonksiyonlar;
using System.Runtime.InteropServices;





namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly VeterinerDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly FaceRecognition _faceRecognition;
        private const string ModelDirectory = "wwwroot/models";
        private const string ShapePredictorPath = "wwwroot/models/shape_predictor_68_face_landmarks.dat";
        private const string FaceRecognitionModelPath = "wwwroot/models/dlib_face_recognition_resnet_model_v1.dat";

        public UserController(VeterinerDBContext context, UserManager<AppUser> userManager, IEmailSender emailSender, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _faceRecognition = FaceRecognition.Create(ModelDirectory);
        }
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var user = await _userManager.GetUserAsync(User);

            HayvanlarBilgiViewModel model = new();
            var sahipOlunanHayvanlar = await model.SahipOlunanHayvanlarinListesiniGetirAsnyc(_context, user);
            
            List<HayvanlarBilgiViewModel> hayvanlar = new();
            foreach (var hayvan in sahipOlunanHayvanlar)
            {
                hayvanlar.Add(await model.HayvanBilgileriniGetirAsync(hayvan,user,_context));
            }

            var tuplle = (user, hayvanlar);
            return View(tuplle);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePaswordViewModel model)
        {

            var user = _userManager.GetUserAsync(User).Result;

            ChangePaswordValidators Validator = new ChangePaswordValidators();
            var result = Validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            var resultChangePasword = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!resultChangePasword.Result.Succeeded)
            {
                ModelState.AddModelError("OldPassword", "Eski şifreniz yanlış.");
                return View();
            }
            else
            {
                user.SifreOlusturmaTarihi = System.DateTime.Now;
                user.SifreGecerlilikTarihi = System.DateTime.Now.AddDays(120);
                await _userManager.UpdateAsync(user);
                TempData["PasswordChanged"] = $"Sayın {user.UserName}, Şifreniz başarıyla değiştirildi.";
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EditUserViewModel model = new();
            model.InsanAdi = user.InsanAdi.ToUpper();
            model.InsanSoyadi = user.InsanSoyadi.ToUpper();
            model.Email = user.Email.ToLower();
            model.UserName = user.UserName.ToUpper();
            model.PhoneNumber = user.PhoneNumber;
            model.Id = user.Id;
            model.ImgURL = user.ImgURL;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEdit(EditUserViewModel model)
        {
            UserEditValidator validator = new();
            ValidationResult result = validator.Validate(model);
            var user = await _context.Users.FindAsync(model.Id);

            EditUserViewModel returnModel = new()
            {
                InsanAdi = model.InsanAdi.ToUpper(),
                InsanSoyadi = model.InsanSoyadi.ToUpper(),
                Email = model.Email.ToLower(),
                UserName = model.UserName.ToUpper(),
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                ImgURL = user.ImgURL

            };

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View("EditUser", returnModel);
            }

            user.InsanAdi = model.InsanAdi.ToUpper();
            user.InsanSoyadi = model.InsanSoyadi.ToUpper();
            user.Email = model.Email.ToLower();
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName.ToUpper();

            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {


                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var userKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", user.Id.ToString());

                if (!Directory.Exists(userKlasoru))
                {
                    Directory.CreateDirectory(userKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(userKlasoru);

                if (eskiFotograflar.Length > 0)
                {
                    foreach (var eskiFotograf in eskiFotograflar)
                    {
                        System.IO.File.Delete(eskiFotograf);
                    }
                }

                var filePath = Path.Combine(userKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/user/{user.Id}/{dosyaAdi}";

                user.ImgURL = fileUrl;

            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }
            else if (model.PhotoOption == "deletePhoto")
            {
                user.ImgURL = null;
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }

            _userManager.UpdateAsync(user).Wait();
            returnModel.ImgURL = user.ImgURL;

            TempData["EditUser"] = $"{user.InsanAdi} {user.InsanSoyadi} isimli kişinin kullanıcı bilgileri başarıyla güncellendi.";

            return View("EditUser", returnModel);
        }

        public async Task<IActionResult> SendDeletionEmail()
        {
            var user = await _userManager.GetUserAsync(User);

            var code = await _userManager.GenerateUserTokenAsync(user, "Default", "DeleteAccount");
            var callbackUrl = Url.Action("DeleteAccount", "User", new { userId = user.Id, code = code }, protocol: Request.Scheme);

            var message = $@"
                    <!DOCTYPE html>
                    <html lang='tr'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Hesap Silme İsteği</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                color: #333;
                                line-height: 1.6;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #fff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }}
                            h1 {{
                                color: #444;
                                font-size: 24px;
                                text-align: center;
                                margin-bottom: 20px;
                            }}
                            p {{
                                font-size: 16px;
                                margin-bottom: 20px;
                            }}
                            a.button {{
                                display: inline-block;
                                background-color: #007bff;
                                color: #fff;
                                padding: 10px 20px;
                                text-decoration: none;
                                border-radius: 5px;
                                font-weight: bold;
                                text-align: center;
                            }}
                            a.button:hover {{
                                background-color: #0056b3;
                            }}
                            .footer {{
                                margin-top: 20px;
                                text-align: center;
                                font-size: 12px;
                                color: #777;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Hesap Silme İsteği</h1>
                            <p>Sayın {User.Identity.Name}, hesabınızı silmek için aşağıdaki butona tıklayın. </p>
                            <p>Sistemde kayıtlı olan bir evcil hayvanınız var ise bilgileri silincektir. Bu işlem geri alınamaz.</p>
                            <p style='text-align:center;'>
                                <a href='{callbackUrl}' class='button'>Hesabımı Sil</a>
                            </p>
                            <p class='footer'>Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayın.</p>
                        </div>
                    </body>
                    </html>";


            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Hesap Silme Talebi", message);
                TempData["MailGonderildi"] = "Hesabınızı silmek için gerekli bağlantı mail adresinize gönderildi. Mail adresinizde bulunan linke tıklayarak hesabınızı silebilirsiniz.";
                return RedirectToAction("Information");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hesabınızı silmek için gerekli bağlantı mail adresinize gönderilemedi. Lütfen sistem yöneticisi ile iletişime geçiniz." + " " + ex.Message;

                return RedirectToAction("Information");
            }


        }

        //TODO kullanıcı silinmeyecek, kullanıcı için aktifMi seklinde bir alan tanımlanacak
        //Kullanıcı hesabı deaktif yapılacak.
        //Bu işlemlerden sonra şifremi unuttum ve login işlemleri düzenlenecek, aktif olan kullanıcılar login olabiliecek.
        //public async Task<IActionResult> DeleteAccount(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("BadRequest");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.VerifyUserTokenAsync(user, "Default", "DeleteAccount", code);
        //    if (!result)
        //    {
        //        return View("BadRequest");
        //    }


        //    // Kullanıcının sahip olduğu hayvan kayıtlarını al
        //    var kullaniciHayvanKayitlari = _context.SahipHayvan
        //        .Where(s => s.AppUser.InsanTckn == user.InsanTckn)
        //        .ToList();

        //    // Kullanıcının sahip olduğu hayvanlara ait ID'leri listele
        //    var kullaniciHayvanIds = kullaniciHayvanKayitlari
        //        .Select(k => k.HayvanId)
        //        .ToList();

        //    // SahipHayvans tablosundan sadece bu kullanıcının kayıtlarını sil
        //    _context.SahipHayvan.RemoveRange(kullaniciHayvanKayitlari);
        //    _context.SaveChanges();

        //    // Sahipsiz kalan hayvanları belirle
        //    var sahipsizHayvanlar = _context.Hayvanlar
        //        .Where(h => !(_context.SahipHayvan
        //            .Any(sh => sh.HayvanId == h.HayvanId)))
        //        .ToList();

        //    // Hayvans tablosundan sahipsiz hayvanları sil
        //    _context.Hayvanlar.RemoveRange(sahipsizHayvanlar);
        //    _context.SaveChanges();


        //    await _signInManager.SignOutAsync();
        //    await _userManager.DeleteAsync(user);

        //    return RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        public IActionResult FaceId()
        {
            return View();
        }

        public async Task<IActionResult> FaceId(IFormFile[] filePhotos)
        {
            if (filePhotos == null || filePhotos.Length <=10)
                return Json(new { success = false, message = "Fotoğraf çekme işlemi başarısız oldu" });

            try
            {
                FaceRecognitionClass faceRecognitionClass = new();


                var result = (new List<FaceEncoding>(), false);
                result = await faceRecognitionClass.DetectFacesAsync(filePhotos);

                if (result.Item2 == false)
                    return Json(new { success = false, message = "Yüz tespit işlemi başarısız oldu." });
                var user = await _userManager.GetUserAsync(User);

                foreach (var faceEncoding in result.Item1)
                {
                    await faceRecognitionClass.SaveFaceEncodingToDatabaseAsync(user.Id, faceEncoding, _context);
                }

                return Json(new { success = true, message = "Yüz tanıma işlemi başarılı oldu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }


    }

}
