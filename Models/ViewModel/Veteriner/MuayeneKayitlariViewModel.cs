using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class MuayeneKayitlariViewModel : Muayene
    {
        public int SonSayfaNumarasi { get; set; }
        public int MevcutSayfa { get; set; }
        public string HayvanAdi { get; set; }
        public string HekimAdi { get; set; }

        public DateTime? ilkTarih { get; set; }
        public DateTime? sonTarih { get; set; }

        public List<Muayene> MuayenelerListesi { get; set; }

        public async Task<List<Muayene>> MuayeneKayitlariniGetirAsync(VeterinerDBContext context)
        {
            MuayenelerListesi = new();
            if (ilkTarih == null)
                ilkTarih = new DateTime(1975, 01, 01);
            MuayenelerListesi = await context.Muayeneler
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
                            (string.IsNullOrWhiteSpace(HayvanAdi) ||
                                m.Hayvan.HayvanAdi
                                    .ToUpper()
                                    .Contains(HayvanAdi.ToUpper())
                            ) && (HayvanId == 0 || m.Hayvan.HayvanId == HayvanId) &&
                                m.MuayeneTarihi >= ilkTarih &&
                                m.MuayeneTarihi <= sonTarih.Value.AddHours(24)
                        )
                   .OrderByDescending(m => m.MuayeneTarihi)
                   .Skip((MevcutSayfa - 1) * 10)
                   .Take(10)
                   .ToListAsync();

            return MuayenelerListesi;
        }

        public async Task<int> ToplamSayfaSayisiniGetirAsync(VeterinerDBContext context)
        {
            if (ilkTarih == null)
                ilkTarih = new DateTime(1975, 01, 01);
            var toplamKayitlar = await context.Muayeneler
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
                            (string.IsNullOrWhiteSpace(HayvanAdi) ||
                                m.Hayvan.HayvanAdi
                                    .ToUpper()
                                    .Contains(HayvanAdi.ToUpper())
                            ) && (HayvanId == 0 || m.Hayvan.HayvanId == HayvanId) &&
                                m.MuayeneTarihi >= ilkTarih &&
                                m.MuayeneTarihi <= sonTarih.Value.AddHours(24)
                        )
                   .ToListAsync();
            int toplamKayitlarSayisi = toplamKayitlar.Count();
            if (toplamKayitlarSayisi % 10 == 0)
                return toplamKayitlarSayisi / 10;
            return (toplamKayitlarSayisi / 10) + 1;
        }

    }
}
