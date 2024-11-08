using System.Collections.Generic;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class IletisimBilgileri
    {
        public IletisimBilgileri()
        {
            SosyalMedyalar = new HashSet<IletisimBilgileriSosyalMedya>();
        }
        public int IletisimBilgileriId { get; set; }
        public string SubeAdi { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string Cadde { get; set; }
        public string Sokak { get; set; }
        public string No { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<IletisimBilgileriSosyalMedya> SosyalMedyalar { get; set; }

    }
}
