using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class AdresDuzenleViewModel :IletisimBilgileri
    {
        public string Imza { get; set; }
        public List<AdresDuzenleViewModel> IletisimBilgileriListesi { get; set; }
        private List<IletisimBilgileri> AdreslerListesi{ get; set; }
        public List<SosyalMedya> SosyalMedyalarListesi { get; set; }



        public async Task<List<AdresDuzenleViewModel>> IletisimBilgileriListesiniGetirAsync(VeterinerDBContext context)
        {
            AdreslerListesi = await context.IletisimBilgileri
                .Include(ib => ib.SosyalMedyalar)
                .ToListAsync();

            SosyalMedyalarListesi = await context.SosyalMedyalar.ToListAsync();

            IletisimBilgileriListesi = AdreslerListesi.Select(al => new AdresDuzenleViewModel
            {
                IletisimBilgileriId = al.IletisimBilgileriId,
                SubeAdi = al.SubeAdi,
                TelefonNumarasi = al.TelefonNumarasi,
                Sehir = al.Sehir,
                Ilce = al.Ilce,
                Mahalle = al.Mahalle,
                Cadde = al.Cadde,
                Sokak = al.Sokak,
                No = al.No,
                Latitude = al.Latitude,
                Longitude = al.Longitude,
                AktifMi = al.AktifMi,
                SosyalMedyalar = al.SosyalMedyalar,
                Imza=Signature.CreateSignature(al.IletisimBilgileriId.ToString(), al.IletisimBilgileriId.ToString()),

                SosyalMedyalarListesi = SosyalMedyalarListesi
            }).ToList();


            return IletisimBilgileriListesi;
        }
    }
}
