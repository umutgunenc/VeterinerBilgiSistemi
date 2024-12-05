using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class MuayeneNoViewModel :Muayene
    {
        public Muayene Muayene { get; set; }

        public List<KullanilanStok> KullanilanIlaclar { get; set; }
        public List<YapilanKanTesti> YapilanKanTestleri { get; set; }
        public List<SelectListItem> Hastaliklar { get; set; }
        public string Imza { get; set; }


        public async Task<Muayene> MuayeneyiGetirAsync(int id, VeterinerDBContext context)
        {
            Muayene = await context.Muayeneler
                .Include(m => m.Hayvan)
                .Include(m => m.Hekim)
                .Include(m => m.StokHareketleri)
                    .ThenInclude(sh => sh.Stok)
                .Include(m => m.KanTestleri)
                    .ThenInclude(kt => kt.KanDegerleri)
                .Include(m=>m.Hastalik)
                .Where(m => m.MuayeneId == id)
                .FirstOrDefaultAsync();

            return Muayene;

        }

        public async Task<List<KullanilanStok>> IlaclarListesiniGetirAsync(VeterinerDBContext context)
        {
            List<Stok> Stoklar = new();

            Stoklar = await context.Stoklar
                .Include(s => s.StokHareketleri)
                    .ThenInclude(sh => sh.Muayene)
                .Include(s=>s.Birim)
                .Include(s=>s.Kategori)
                .Where(s=>s.Kategori.IlacMi==true)
                .ToListAsync();

            KullanilanIlaclar = new();

            foreach (Stok stok in Stoklar)
            {
                KullanilanStok seciliStok = new();

                seciliStok.Stok = stok;

                if (stok.StokHareketleri.Any(sh => sh.Muayene == Muayene))
                    seciliStok.YapildiMi = true;

                KullanilanIlaclar.Add(seciliStok);
            }

            return KullanilanIlaclar;
        }

        public async Task<List<YapilanKanTesti>> KanTestleriListesiniGetirAsync(VeterinerDBContext context)
        {
            List<KanDegerleri> KanTestleri = new() ;

            KanTestleri = await context.KanDegerleri
                .Include(kd => kd.Muayeneler)
                    .ThenInclude(m=>m.Muayene)
                .ToListAsync();

            YapilanKanTestleri = new();

            foreach (KanDegerleri kanTesti in KanTestleri)
            {
                YapilanKanTesti seciliKanTesti = new();

                seciliKanTesti.KanDegerleri = kanTesti;

                if (kanTesti.Muayeneler.Any(km=>km.Muayene==Muayene))
                    seciliKanTesti.SecildiMi = true;

                YapilanKanTestleri.Add(seciliKanTesti);
            }

            return YapilanKanTestleri;
        }

        public async Task<List<SelectListItem>> HastalikListesiniGetirAsync(VeterinerDBContext context) {

            var hastaliklar = await context.Hastaliklar.ToListAsync();
            Hastaliklar = new();
            foreach (var hastalik in hastaliklar)
            {
                Hastaliklar.Add(new SelectListItem
                {
                    Text = hastalik.HastalikAdi.ToUpper(),
                    Value = hastalik.HastalikId.ToString()
                });

            }

            return Hastaliklar;
        }

    }
    public class YapilanKanTesti : KanTestiMuayene
    {
        public bool SecildiMi { get; set; }
    }

    public class KullanilanStok :StokHareket
    {
        public bool YapildiMi { get; set; }
    }

}
