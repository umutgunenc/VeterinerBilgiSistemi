using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Enum;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using VeterinerBilgiSistemi.Models.ViewModel.Animal;

namespace VeterinerBilgiSistemi.Models.Validators.ValidateFunctions
{
    public static class FunctionsValidator
    {

        private static readonly VeterinerDBContext _context = new();

        public static bool BeRenk(int Id)
        {
            return _context.Renkler.Any(x => x.RenkId == Id);
        }
        public static bool BeCins(int id)
        {
            return _context.Cinsler.Any(x => x.CinsId == id);
        }
        public static bool BeTur(int TurId)
        {
            return _context.Turler.Any(x => x.TurId == TurId);
        }
        public static bool BeTurCins(int id)
        {
            return _context.CinsTur.Any(x => x.Id == id);
        }
        public static bool BeKategori(int kategoriId)
        {
            return _context.Kategoriler.Any(x => x.KategoriId == kategoriId);
        }
        public static bool BeMatchedCinsTur(int cinsId, int turId)
        {
            return _context.CinsTur.Any(x => x.CinsId == cinsId && x.TurId == turId);
        }
        public static bool BeSameCins(AddAnimalViewModel model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvanlar.Where(h => h.HayvanId == parentID).FirstOrDefault();
            if (parent == null)
                return false;

            var parentCinsId = _context.CinsTur.Where(ct => ct.Id == parent.CinsTurId).Select(ct => ct.CinsId).FirstOrDefault();


            return parentCinsId == model.SecilenCinsId;
        }
        public static bool BeSameCins(EditAnimalViewModel model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvanlar.Where(h => h.HayvanId == parentID).FirstOrDefault();
            if (parent == null)
                return false;

            var parentCinsId = _context.CinsTur.Where(ct => ct.Id == parent.CinsTurId).Select(ct => ct.CinsId).FirstOrDefault();


            return parentCinsId == model.CinsId;
        }
        public static bool BeOlder<T>(T model, int? parentID) where T : Hayvan
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvanlar.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.HayvanDogumTarihi < model.HayvanDogumTarihi;
        }
        public static bool BeGirl(int? anneId)
        {
            if (!anneId.HasValue)
                return true;

            var anne = _context.Hayvanlar.FirstOrDefault(a => a.HayvanId == anneId.Value);
            return anne != null && anne.HayvanCinsiyet == Cinsiyet.Dişi;
        }
        public static bool BeBoy(int? babaId)
        {
            if (!babaId.HasValue)
                return true;

            var baba = _context.Hayvanlar.FirstOrDefault(a => a.HayvanId == babaId.Value);
            return baba != null && baba.HayvanCinsiyet == Cinsiyet.Erkek;
        }
        public static bool BeRol(int rolId)
        {
            return _context.Roles.Any(x => x.Id == rolId);
        }
        public static bool BeAllowedRol(string roller)
        {
            if (string.IsNullOrEmpty(roller))
                return true;
            roller = roller.ToUpper();
            if (roller == "admin".ToUpper() || roller == "veteriner".ToUpper() || roller == "çalışan".ToUpper() || roller == "müşteri".ToUpper())
                return true;
            return false;
        }
        public static bool BeValidRadioPhotoAdd(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return radioValues.Contains(value);
        }
        public static bool BeValidExtensionForPhoto(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensionsForPhoto.Contains(extension);
        }
        public static bool BeUsedTCKN(string girilenTCKN)
        {
            if (string.IsNullOrEmpty(girilenTCKN))
                return true;

            return _context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool BeValidTCKN(string tcKimlikNo)
        {

            if (string.IsNullOrEmpty(tcKimlikNo))
                return true;


            bool returnvalue = false;
            if (tcKimlikNo.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tcKimlikNo);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }
        public static bool BeValidPasswordDate(string username, string password)
        {
            if (username == null || password == null)
                return false;

            return _context.Users.Any(x => x.UserName.ToUpper() == username.ToUpper() && x.SifreGecerlilikTarihi >= DateTime.Now);
        }
        public static bool BeRegisteredParentAnimal(int? animalId)
        {
            return !animalId.HasValue || _context.Hayvanlar.Any(a => a.HayvanId == animalId.Value);
        }
        public static bool BeValidHayvan(int hayvanId)
        {
            return _context.Hayvanlar.Any(h => h.HayvanId == hayvanId);
        }
        public static bool BeBirim(int birimId)
        {
            return _context.Birimler.Any(b => b.BirimId == birimId);
        }
        public static bool BeOwnedByCurrentUser(EditAnimalViewModel model, int hayvanId)
        {
            return _context.SahipHayvan.Any(x => x.HayvanId == hayvanId && x.AppUser.InsanTckn == model.SahipTckn);
        }
        public static bool BeInStock(string barkod)
        {
            if (string.IsNullOrEmpty(barkod))
                return true;
            return _context.Stoklar.Any(x => x.StokBarkod.ToUpper() == barkod.ToUpper());
        }
        public static bool BePositiveStock(StokCikisKaydetViewModel model, double? miktar)
        {
            var stokHarektlerListesi = _context.StokHareketler
                .Where(sh => sh.StokId == model.StokId)
                .ToList();
            double? toplamAlisMiktari = 0;
            double? toplamSatisMiktari = 0;
            foreach (var stokHareket in stokHarektlerListesi)
            {
                stokHareket.StokGirisAdet ??= 0;
                stokHareket.StokCikisAdet ??= 0;
                toplamAlisMiktari += stokHareket.StokGirisAdet;
                toplamSatisMiktari += stokHareket.StokCikisAdet;
            }

            if (((toplamSatisMiktari + miktar) >= toplamAlisMiktari) || toplamAlisMiktari <= 0)
                return false;
            return true;
        }

        public static bool BeHastalik(int hastalikId)
        {
            return _context.Hastaliklar.Any(h => h.HastalikId == hastalikId);
        }

        public static bool BeKanTahlili(int tahlilId)
        {
            return _context.KanDegerleri.Any(x => x.KanDegerleriId == tahlilId);
        }
        public static bool BeIletisimBilgileri(int id)
        {
            return _context.IletisimBilgileri.Any(x => x.IletisimBilgileriId == id);
        }

        public static bool SeacrhInStock(string arananMetin)
        {
            if (string.IsNullOrEmpty(arananMetin))
                return false;
            arananMetin = arananMetin.ToUpper();

            return _context.Stoklar
                    .Where(s => (s.StokBarkod.ToUpper().Contains(arananMetin) ||
                                 s.StokAdi.ToUpper().Contains(arananMetin)) &&
                                 s.AktifMi == true)
                    .Any();

        }
        public static bool LoginSucceed(AppUser user)
        {
            return _context.Users.Any(x => x.UserName == user.UserName && x.PasswordHash == user.PasswordHash);

        }
        //public static async Task<bool> BeCorrectOldPasswordAsync(string oldPassword, string userName, UserManager<AppUser> userManager, CancellationToken cancellationToken)
        //{
        //    var user = await userManager.FindByNameAsync(userName);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return await userManager.CheckPasswordAsync(user, oldPassword);
        //}

        public static bool BeUniqueAnaSayfaFotografAdi(string fotografAdi)
        {
            if (string.IsNullOrEmpty(fotografAdi))
                return true;
            return !_context.AnaSayfaFotograflar.Any(x => x.FotografAdi.ToUpper() == fotografAdi.ToUpper());
        }
        public static bool BeUniqueKategori(string kategoriAdi)
        {
            if (string.IsNullOrEmpty(kategoriAdi))
                return true;

            return !_context.Kategoriler.Any(x => x.KategoriAdi.ToUpper() == kategoriAdi.ToUpper());
        }
        public static bool BeUniqueTCKN(string girilenTCKN)
        {
            if (string.IsNullOrEmpty(girilenTCKN))
                return true;

            return !_context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool BeUniqueCins(string girilenDeger)
        {
            if (string.IsNullOrEmpty(girilenDeger))
                return true;

            return !_context.Cinsler.Any(x => x.CinsAdi.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool BeUniqueTur(string girilenTur)
        {
            if (string.IsNullOrEmpty(girilenTur))
                return true;
            return !_context.Turler.Any(t => t.TurAdi.ToUpper() == girilenTur.ToUpper());
        }
        public static bool BeUniqueRol(string rol)
        {
            if (string.IsNullOrEmpty(rol))
                return true;
            return !_context.Roles.Any(x => x.Name.ToUpper() == rol.ToUpper());
        }
        public static bool BeUniqueRenk(string girilenDeger)
        {
            if (string.IsNullOrEmpty(girilenDeger))
                return true;
            return !_context.Renkler.Any(x => x.RenkAdi.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool BeUniqueTel(string TelNo)
        {
            if (string.IsNullOrEmpty(TelNo))
                return true;
            return !_context.Users.Any(x => x.PhoneNumber.ToUpper() == TelNo.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki telefon numarası benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insanTel"></param>
        /// <returns></returns>
        public static bool BeUniqueTel(int kisiId, string telefonNo)
        {
            if (string.IsNullOrEmpty(telefonNo))
                return true;
            return !_context.Users.Any(x => x.PhoneNumber == telefonNo && x.Id != kisiId);
        }
        public static bool BeUniqueOrNullDiplomaNo(string diplomaNumarasi)
        {
            if (string.IsNullOrEmpty(diplomaNumarasi))
                return true;

            return !_context.Users.Any(x => x.DiplomaNo.ToUpper() == diplomaNumarasi.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki diploma numarası benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="diplomaNo"></param>
        /// <returns></returns>
        public static bool BeUniqueOrNullDiplomaNo(int id, string diplomaNo)
        {
            if (string.IsNullOrEmpty(diplomaNo))
                return true;

            return !_context.Users.Any(x => x.DiplomaNo == diplomaNo && x.Id != id);
        }
        public static bool BeUniqueKullaniciAdi(string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;
            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki kullanıcı adı benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kullaniciAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueKullaniciAdi(int id, string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;

            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper() && x.Id != id);
        }
        public static bool BeUniqueEmail(string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Users.Any(x => x.Email.ToUpper() == insanMail.ToUpper() || x.Email.ToLower() == insanMail.ToLower());
        }
        /// <summary>
        /// Girilen kişi dışındaki mail adresi bezersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insanMail"></param>
        /// <returns></returns>
        public static bool BeUniqueEmail(int id, string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Users.Any(x => x.Email.ToUpper() == insanMail.ToUpper() && x.Id != id);
        }
        public static bool BeUniqueBirim(string birimAdi)
        {
            if (string.IsNullOrEmpty(birimAdi))
                return true;
            return !_context.Birimler.Any(x => x.BirimAdi.ToUpper() == birimAdi.ToUpper());
        }
        public static bool BeUniqueBarkod(string barkod)
        {
            if (string.IsNullOrEmpty(barkod))
                return true;
            return !_context.Stoklar.Any(x => x.StokBarkod.ToUpper() == barkod);
        }
        /// <summary>
        /// Girilen stok dışındaki barkod numarası benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="barkod"></param>
        /// <returns></returns>
        public static bool BeUniqueBarkod(int stokId, string barkod)
        {
            if (string.IsNullOrEmpty(barkod))
                return true;
            return !_context.Stoklar.Any(x => x.StokBarkod.ToUpper() == barkod.ToUpper() && x.Id != stokId);
        }
        public static bool BeUniqueStokAdi(string stokAdi)
        {
            if (string.IsNullOrEmpty(stokAdi))
                return true;
            return !_context.Stoklar.Any(x => x.StokAdi.ToUpper() == stokAdi.ToUpper());
        }
        /// <summary>
        /// Girilen stok dışındaki stok ismi benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="StokAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueStokAdi(int stokId, string stokAdi)
        {
            if (string.IsNullOrEmpty(stokAdi))
                return true;
            return !_context.Stoklar.Any(x => x.StokAdi.ToUpper() == stokAdi.ToUpper() && x.Id != stokId);
        }
        public static bool BeUniqueHastalikAdi(string hastalikAdi)
        {
            if (string.IsNullOrEmpty(hastalikAdi))
                return true;

            return !_context.Hastaliklar.Any(x => x.HastalikAdi.ToUpper() == hastalikAdi.ToUpper());
        }


        public static bool BeUniqueKanTathlilAdi(string tathlilAdi)
        {
            if (string.IsNullOrEmpty(tathlilAdi))
                return true;
            return !_context.KanDegerleri.Any(x => x.KanTestiAdi.ToUpper() == tathlilAdi.ToUpper());
        }
        /// <summary>
        /// Girilen kan tahlili ismi, kendi ismi dışında benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tathlilAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueKanTathlilAdi(int id, string tathlilAdi)
        {
            if (string.IsNullOrEmpty(tathlilAdi))
                return true;
            return !_context.KanDegerleri.Any(x => x.KanTestiAdi.ToUpper() == tathlilAdi.ToUpper() && x.KanDegerleriId != id);
        }
        public static bool BeUniqueTitle( string baslik)
        {
            if (string.IsNullOrEmpty(baslik))
                return true;
            return !_context.Hakkimizda.Any(x => x.Baslik.ToUpper() == baslik.ToUpper());
        }

        /// <summary>
        /// Girilen başlık,kendi dışında benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tathlilAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueTitle(int id,string baslik)
        {
            if (string.IsNullOrEmpty(baslik))
                return true;
            return !_context.Hakkimizda.Any(x => x.Baslik.ToUpper() == baslik.ToUpper() && x.HakkimizdaId != id);
        }


        public static bool BeUniqueSosyalMedyaAdi(string sosyalMedyaAdi)
        {
            if (string.IsNullOrEmpty(sosyalMedyaAdi))
                return true;
            return !_context.SosyalMedyalar.Any(x => x.SosyalMedyaAdi.ToUpper() == sosyalMedyaAdi.ToUpper());
        }

        /// <summary>
        /// Girilen sosyal medya adı,kendi dışında benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sosyalMedyaAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueSosyalMedyaAdi(int id, string sosyalMedyaAdi)
        {
            if (string.IsNullOrEmpty(sosyalMedyaAdi))
                return true;
            return !_context.SosyalMedyalar.Any(x => x.SosyalMedyaAdi.ToUpper() == sosyalMedyaAdi.ToUpper() && x.SosyalMedyaId != id);
        }

        public static bool BeUniqueSubeAdi(string subeAdi)
        {
            if (string.IsNullOrEmpty(subeAdi))
                return true;
            return !_context.IletisimBilgileri.Any(x => x.SubeAdi.ToUpper() == subeAdi.ToUpper());
        }

        /// <summary>
        /// Girilen sube adi,kendi dışında benzersiz olmalıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subeAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueSubeAdi(int id, string subeAdi)
        {
            if (string.IsNullOrEmpty(subeAdi))
                return true;
            return !_context.IletisimBilgileri.Any(x => x.SubeAdi.ToUpper() == subeAdi.ToUpper() && x.IletisimBilgileriId != id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolId">int rol Id </param>
        /// <param name="validRoles">istenen rollerin isim listesi, küçük harf ve varsa türkçe karakter kullanilmali</param>
        /// <returns></returns>
        public static bool IsRoleMatching(int rolId, List<string> validRoles)
        {
            var role = _context.Roles.Find(rolId);
            return role != null && validRoles.Contains(role.Name.ToLower());
        }


        public static bool BeNotUsedRenk(int Id)
        {
            return !_context.Hayvanlar.Any(x => x.RenkId == Id);
        }
        public static bool BeNotUsedBirim(int birimId)
        {
            return !_context.Stoklar.Any(x => x.BirimId == birimId);
        }
        public static bool BeNotUsedTurCins(int id)
        {
            var cinsId = _context.CinsTur.Where(x => x.CinsId == id).Select(x => x.CinsId).FirstOrDefault();
            var turId = _context.CinsTur.Where(x => x.TurId == id).Select(x => x.TurId).FirstOrDefault();
            return !_context.Hayvanlar.Where(x => x.CinsTur.CinsId == cinsId && x.CinsTur.Tur.TurId == turId).Any();
        }
        public static bool BeNotMatchedRol(int rolId)
        {
            return !_context.UserRoles.Any(x => x.RoleId == rolId);
        }
        public static bool BeNotMatchedCins(int cinsId)
        {
            return !_context.CinsTur.Any(x => x.CinsId == cinsId);
        }
        public static bool BeNotMatchTurCins(int turId)
        {
            return !_context.CinsTur.Where(x => x.TurId == turId).Any();
        }
        public static bool BeNotMatchedTur(int turId)
        {
            return !_context.CinsTur.Any(x => x.TurId == turId);
        }
        public static bool BeNotMatchedKategori(int kategoriId)
        {
            return !_context.Stoklar.Any(x => x.KategoriId == kategoriId);
        }
        public static bool BeNotOwnedAnimal(int hayvanId, string yeniSahipTCKN)
        {
            if (string.IsNullOrEmpty(yeniSahipTCKN))
                return true;
            return !_context.SahipHayvan.Where(x => x.AppUser.InsanTckn == yeniSahipTCKN && x.HayvanId == hayvanId && x.AktifMi == true).Any();
        }

        public static bool BeNotUsedHastalik(int hastalikId)
        {
            return !_context.HastalikMuayene.Any(x => x.HastalikId == hastalikId);
        }

        public static bool BeNotUsedKanTahlili(int tahlilId)
        {
            return !_context.KanTestiMuayene.Any(x => x.KanDegerleriId == tahlilId);
        }





        private static readonly List<string> radioValues = new List<string>
        {
            "keepPhoto",
            "changePhoto",
            "deletePhoto"
        };
        private static readonly List<string> allowedExtensionsForPhoto = new List<string>
        {   ".jpg",
            ".jpeg",
            ".png",
            ".bmp"
        };

    }
}

