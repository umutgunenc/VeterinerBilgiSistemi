using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Muayene
    {
        public Muayene()
        {
            StokHareketleri = new HashSet<StokHareket>();
            KanTestleri = new HashSet<KanTestiMuayene>();
        }
        public int MuayeneId { get; set; }
        public int HayvanId { get; set; }
        public DateTime MuayeneTarihi { get; set; }
        public DateTime? SonrakiMuayeneTarihi { get; set; }
        public string Aciklama { get; set; }
        public int HekimId { get; set; }
        public int HastalikId { get; set; }
        public int? YapayZekaTahminId { get; set; }
        public virtual Hayvan Hayvan { get; set; }
        public virtual AppUser Hekim { get; set; }
        public virtual Hastalik Hastalik { get; set; }
        public virtual Hastalik YapayZekaTahminHastaligi { get; set; }
        public virtual ICollection<KanTestiMuayene> KanTestleri { get; set; }
        public virtual ICollection<StokHareket>? StokHareketleri { get; set; }

    }
}
