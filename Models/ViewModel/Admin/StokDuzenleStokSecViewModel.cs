using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class StokDuzenleStokSecViewModel :Stok
    {
        public string GirilenBarkodNo { get;set; }

        public StokDuzenleStokSecViewModel ModeliOlustur(StokDuzenleKaydetViewModel model)
        {

            Aciklama = model.Aciklama;
            AktifMi = model.AktifMi;
            StokAdi = model.StokAdi;
            StokBarkod = model.StokBarkod;
            BirimId = model.BirimId;
            KategoriId = model.KategoriId;

            return this;           
            
        }
    }
}
