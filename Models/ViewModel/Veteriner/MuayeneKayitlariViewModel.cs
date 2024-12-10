using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.ViewModel.Animal;

namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class MuayeneKayitlariViewModel : Muayene
    {
        public int SonSayfaNumarasi { get; set; }
        public int MevcutSayfa { get; set; }
        public string HayvanAdi { get; set; }
        public string HekimAdi { get; set; }
        public string HekimAdiSoyadi { get; set; }
        public string HastalikAdi { get; set; }
        public string HayvanAdiTablo { get; set; }
        public string SahipAdiSoyadi { get; set; }




        public DateTime? ilkTarih { get; set; }
        public DateTime? sonTarih { get; set; }

        public List<MuayeneKayitlariViewModel> MuayenelerListesi { get; set; }
        private IQueryable<MuayeneKayitlariViewModel> ButunMuayeneListesi { get; set; }
        private IQueryable<MuayeneKayitlariViewModel> ButunMuaayeneListesiniGetir(VeterinerDBContext context)
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
                        m.Hekim.InsanAdi.ToUpper().Contains(HekimAdi.ToUpper()) ||
                        m.Hekim.InsanSoyadi.ToUpper().Contains(HekimAdi.ToUpper())
                    ) &&
                    (string.IsNullOrWhiteSpace(HayvanAdi) ||
                        m.Hayvan.HayvanAdi.ToUpper().Contains(HayvanAdi.ToUpper())
                    ) &&
                    (HayvanId == 0 || m.Hayvan.HayvanId == HayvanId) &&
                    m.MuayeneTarihi >= ilkTarih &&
                    m.MuayeneTarihi <= sonTarih.Value.AddHours(24)
                )
                .OrderByDescending(m => m.MuayeneTarihi)
                .Select(m => new MuayeneKayitlariViewModel
                {
                    MuayeneId = m.MuayeneId,
                    MuayeneTarihi = m.MuayeneTarihi,
                    HayvanAdiTablo = m.Hayvan.HayvanAdi.ToUpper(),
                    SahipAdiSoyadi = (m.Hayvan.Sahipler.FirstOrDefault().AppUser.InsanAdi + " "+ m.Hayvan.Sahipler.FirstOrDefault().AppUser.InsanAdi).ToUpper(),
                    HekimAdiSoyadi = (m.Hekim.InsanAdi + " " + m.Hekim.InsanSoyadi).ToUpper(),
                    HastalikAdi = m.Hastalik.HastalikAdi

                });

            return query;
        }
        public async Task<List<MuayeneKayitlariViewModel>> MuayeneKayitlariniGetirAsync(VeterinerDBContext context)
        {
            ButunMuayeneListesi = ButunMuaayeneListesiniGetir(context);
            MuayenelerListesi = await ButunMuayeneListesi
                   .Skip((MevcutSayfa - 1) * 10)
                   .Take(10)
                   .ToListAsync();

            return MuayenelerListesi;
        }
        public int ToplamSayfaSayisiniGetir()
        {
            int toplamKayitlarSayisi = ButunMuayeneListesi.Count();
            if (toplamKayitlarSayisi == 0)
                return 1;
            else if (toplamKayitlarSayisi % 10 == 0)
                return toplamKayitlarSayisi / 10;
            else
                return (toplamKayitlarSayisi / 10) + 1; ;
        }

    }
}
