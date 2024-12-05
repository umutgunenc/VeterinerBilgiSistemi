using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class StokDuzenleStokSecViewModel :Stok
    {
        public string GirilenBarkodNo { get;set; }

        public StokDuzenleStokSecViewModel ModeliOlustur( )
        {

            Aciklama = Aciklama;
            AktifMi = AktifMi;
            StokAdi = StokAdi;
            StokBarkod = StokBarkod;
            BirimId = BirimId;
            KategoriId = KategoriId;

            return this;           
            
        }
    }
}
