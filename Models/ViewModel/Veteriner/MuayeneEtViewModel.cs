using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class MuayeneEtViewModel : Muayene
    {
        public string Imza { get; set; }
        public List<SeciliStoklar> MuayenedeKullanilanStoklar { get; set; }
        public List<SeciliKanTestleri> MuayendeYapilanKanTestleri { get; set; }
        public List<SelectListItem> HastalikListesi { get; set; }
        public List<Stok> StokLarListesi { get; set; }
        public List<KanDegerleri> KanTestleriListesi { get; set; }

        public async Task<List<Muayene>> MuayenedeKullanianStoklarinListesiAsync(VeterinerDBContext context)
        {
            return await context.Muayeneler
                .Include(m => m.StokHareketleri)
                    .ThenInclude(sh => sh.Stok)
                .Include(m => m.Hekim)
                .Include(m => m.KanTestleri)
                .ToListAsync();
        }

        public async Task<Hayvan> MuayeneOlacakHayvaniGetirAsync(string hayvanId, VeterinerDBContext context)
        {
            if (int.TryParse(hayvanId, out int id))
                return await context.Hayvanlar.FindAsync(id);
            return null;
        }

        public async Task<List<Stok>> StoklarinListesiniGetirAsync(VeterinerDBContext context)
        {
            return await context.Stoklar
                .Include(s => s.Kategori)
                .Where(s => s.Kategori.IlacMi == true && s.AktifMi == true)
                .Include(s => s.Birim)
                .Include(s => s.StokHareketleri)
                    .ThenInclude(sh => sh.Muayene)
                .ToListAsync();
        }

        public async Task<List<KanDegerleri>> MuayenedeYapilacakKanTestlerininListeisiniGetirAsync(VeterinerDBContext context)
        {
            return await context.KanDegerleri
                .Include(kd => kd.Muayeneler)
                    .ThenInclude(ms => ms.Muayene)
                .Where(kd => kd.AktifMi == true).ToListAsync();
        }

        public async Task<List<SelectListItem>> HastaliklarListesiniGetirAsync(VeterinerDBContext context)
        {
            HastalikListesi = new();

            var hastaliklar = await context.Hastaliklar.ToListAsync();
            foreach (var hastalik in hastaliklar)
            {
                HastalikListesi.Add(
                    new SelectListItem
                    {
                        Text = hastalik.HastalikAdi,
                        Value = hastalik.HastalikId.ToString()
                    });
            }

            return HastalikListesi;
        }
    }

    public class SeciliStoklar : StokHareket
    {
        public bool SeciliMi { get; set; }
    }
    public class SeciliKanTestleri : KanTestiMuayene
    {
        public bool SeciliMi { get; set; }
    }

}
