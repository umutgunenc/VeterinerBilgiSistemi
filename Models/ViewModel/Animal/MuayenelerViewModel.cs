using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.Models.ViewModel.Animal
{
    public class MuayenelerViewModel : Muayene
    {
        public List<Muayene> MuayenelerListesi { get;set; }
        public string HekimAdi { get; set; }
        public DateTime? ilkTarih { get; set; } = new DateTime(1975, 01, 01);
        public DateTime? sonTarih { get; set; } = DateTime.Now;
        public int MevcutSayfa { get; set; } = 1;
        public int SonSayfaNumarasi { get; set; }

        public async Task<List<Muayene>> MuayeneKayitlariniGetirAsync(VeterinerDBContext context)
        {
            MuayenelerListesi = new();

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
                            (m.Hayvan != null && m.Hayvan.HayvanId == HayvanId) &&
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
                            (m.Hayvan != null && m.Hayvan.HayvanId == HayvanId) &&
                                m.MuayeneTarihi >= ilkTarih &&
                                m.MuayeneTarihi <= sonTarih.Value.AddHours(24)
                        )
                   .ToListAsync();

            int toplamKayitlarSayisi = toplamKayitlar.Count();
            if (toplamKayitlarSayisi == 0)
                return 1;
            else if (toplamKayitlarSayisi % 10 == 0)
                return toplamKayitlarSayisi / 10;
            else
                return (toplamKayitlarSayisi / 10) + 1;
        }



    }
}
