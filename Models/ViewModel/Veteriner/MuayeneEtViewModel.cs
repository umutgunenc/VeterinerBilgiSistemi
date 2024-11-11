using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class MuayeneEtViewModel : Muayene
    {
        public List<Hastalik> HastalikListesi { get; set; }
        public List<Stok> StokLarListesi { get; set; }
        public List<KanDegerleri> KanTestleriListesi { get; set; }

        public async Task<Hayvan> MuayeneOlacakHayvaniGetirAsync(string hayvanId, VeterinerDBContext context)
        {
            if (int.TryParse(hayvanId, out int id))
                return await context.Hayvanlar.FindAsync(id);
            return null;
        }

        public async Task<List<Stok>> MuayenedeKullanilacakStoklarinListesiniGetirAsync(VeterinerDBContext context)
        {
            return await context.Stoklar
                .Include(s => s.Kategori)
                .Where(s => s.Kategori.IlacMi == true && s.AktifMi == true)
                .Include(s=>s.Birim)
                .Include(s=>s.StokHareketleri)
                .Include(s=>s.Muayeneler)
                    .ThenInclude(s=>s.Muayene)
                .ToListAsync();
        }

        public async Task<List<KanDegerleri>> MuayenedeYapilacakKanTestlerininListeisiniGetirAsync(VeterinerDBContext context)
        {
            return await context.KanDegerleri.Where(kd => kd.AktifMi == true).ToListAsync();
        }

        public async Task<List<Hastalik>> HastaliklarListesiniGetirAsync(VeterinerDBContext context)
        {
            return await context.Hastaliklar.ToListAsync();
        }
    }
}
