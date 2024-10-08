using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class StokDuzenleKaydetViewModel : Stok
    {

        //TODO _contex'i D.I yapınca controlerda birimlistesinigetir ve katergorilistesini getir fonksiyonları çalışmıyor ?
        public string Signature { get; set; }
        private StokDetayViewModel Model { get; set; }
        public List<SelectListItem> KategoriListesi { get; set; }
        public List<SelectListItem> BirimListesi { get; set; }
        public async Task<List<SelectListItem>> KategoriListesiniGetirAsync(VeterinerDBContext context)
        {
            KategoriListesi = new();
            var kategoriler = await context.Kategoriler.ToListAsync();

            foreach (var kategori in kategoriler)
            {
                KategoriListesi.Add(new SelectListItem
                {
                    Text = kategori.KategoriAdi,
                    Value = kategori.KategoriId.ToString()

                });
            }

            return KategoriListesi;
        }
        public async Task<List<SelectListItem>> BirimListesiniGetirAsync(VeterinerDBContext context)
        {
            BirimListesi = new();
            var birimler = await context.Birimler.ToListAsync();

            foreach (var birim in birimler)
            {
                BirimListesi.Add(new SelectListItem
                {
                    Text = birim.BirimAdi,
                    Value = birim.BirimId.ToString()

                });
            }

            return BirimListesi;
        }
        public async Task<StokDuzenleKaydetViewModel> ModeliOlusturAsync(VeterinerDBContext context, StokDuzenleStokSecViewModel model)
        {
            var stok = await context.Stoklar
                .Where(x => x.StokBarkod.ToUpper() == model.GirilenBarkodNo.ToUpper())
                .FirstOrDefaultAsync();

            Id = stok.Id;
            StokAdi = stok.StokAdi;
            StokBarkod = stok.StokBarkod;
            Aciklama = stok.Aciklama;
            AktifMi = stok.AktifMi;
            BirimId = stok.BirimId;
            KategoriId = stok.KategoriId;
            KategoriListesi = await KategoriListesiniGetirAsync(context);
            BirimListesi = await BirimListesiniGetirAsync(context);
            return this;

        }


    }
}
