using System.Collections.Generic;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class SosyalMedya
    {

        public SosyalMedya()
        {
            IletisimBilgileri = new HashSet<IletisimBilgileriSosyalMedya>();
        }
        public int SosyalMedyaId { get; set; }
        public string SosyalMedyaAdi { get; set; }
        public string SosyalMedyaUrl { get; set; }
        public string SosyalMedyaPhotoUrl { get; set; }
        public virtual ICollection<IletisimBilgileriSosyalMedya> IletisimBilgileri { get; set; }
    }
}
