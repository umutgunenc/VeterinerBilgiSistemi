using System.Collections.Generic;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class KanDegerleri
    {
        public KanDegerleri()
        {
            Muayeneler = new HashSet<KanTestiMuayene>();
        }
        public int KanDegerleriId { get; set; }
        public string KanTestiAdi { get; set; }
        public string KanTestiBirimi { get; set; }
        public double? AltLimit { get; set; }
        public double? UstLimit { get; set; }
        public bool AktifMi { get; set; }
        public virtual ICollection<KanTestiMuayene> Muayeneler { get; set; }

    }

}
