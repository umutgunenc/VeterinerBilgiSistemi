using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Animal
{
    public class MuayenelerViewModel : Muayene
    {
        public List<MuayenelerViewModel> MuayenelerListesi { get; set; }
        public string HekimAdi { get; set; }
        public string HayvanAdi { get; set; }
        public string HekimAdiSoyadi { get; set; }
        public string HastalikAdi { get; set; }
        public DateTime? ilkTarih { get; set; }
        public DateTime? sonTarih { get; set; } = DateTime.Now;
        public int MevcutSayfa { get; set; } = 1;
        public int SonSayfaNumarasi { get; set; }
        private IQueryable<MuayenelerViewModel> ButunMuayenelerListesi { get; set; }

        private IQueryable<MuayenelerViewModel> ButunMuayenelerListesiniGetir(VeterinerDBContext context)
        {
            if (ilkTarih == null)
                ilkTarih = new DateTime(1975, 01, 01);
            
            var query = context.Muayeneler
                   .Include(m => m.Hastalik)
                   .Include(m => m.Hekim)
                   .Include(m => m.Hayvan)
                       .ThenInclude(h => h.Sahipler)
                       .ThenInclude(s => s.AppUser)
                   .Where(m =>
                            (string.IsNullOrWhiteSpace(HekimAdi) ||
                                m.Hekim.InsanAdi
                                    .ToUpper()
                                    .Contains(HekimAdi.ToUpper()) ||
                                m.Hekim.InsanSoyadi
                                    .ToUpper()
                                    .Contains(HekimAdi.ToUpper())
                            ) &&
                            (m.Hayvan != null && m.Hayvan.HayvanId == HayvanId) &&
                                m.MuayeneTarihi >= ilkTarih &&
                                m.MuayeneTarihi <= sonTarih.Value.AddHours(24)
                        )
                   .OrderByDescending(m => m.MuayeneTarihi)
                   .Select(m => new MuayenelerViewModel
                   {
                       MuayeneId=m.MuayeneId,
                       HayvanAdi = m.Hayvan.HayvanAdi.ToUpper(),
                       HekimAdiSoyadi = (m.Hekim.InsanAdi + " " + m.Hekim.InsanSoyadi).ToUpper(),
                       MuayeneTarihi = m.MuayeneTarihi,
                       SonrakiMuayeneTarihi = m.SonrakiMuayeneTarihi,
                       HastalikAdi = m.Hastalik.HastalikAdi.ToUpper()
                   });

            return query;
        }

        public async Task<List<MuayenelerViewModel>> MuayeneKayitlariniGetirAsync(VeterinerDBContext context)
        {
            ButunMuayenelerListesi = ButunMuayenelerListesiniGetir(context);
            MuayenelerListesi = await ButunMuayenelerListesi
                   .Skip((MevcutSayfa - 1) * 10)
                   .Take(10)
                   .ToListAsync();

            return MuayenelerListesi;
        }

        public int ToplamSayfaSayisiniGetir()
        {
            int toplamKayitlarSayisi = ButunMuayenelerListesi.Count();
            if (toplamKayitlarSayisi == 0)
                return 1;
            else if (toplamKayitlarSayisi % 10 == 0)
                return toplamKayitlarSayisi / 10;
            else
                return (toplamKayitlarSayisi / 10) + 1;
        }



    }
}
