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
        // Bir muayenede bir hayvan muayene edilir
        public virtual Hayvan Hayvan { get; set; }
        // Bir muayeneyi bir hekim yapar
        public virtual AppUser Hekim { get; set; }
        // Bir muayenede bir hastalık teshisi konur
        public virtual Hastalik Hastalik { get; set; }
        // Bir muayende bir YapayZeka Tahmini olabilir
        public virtual Hastalik YapayZekaTahminHastaligi { get; set; }

        //Kan Testi Tablosu ile n-n ilişki Ara Tablo Olarak KanTestiMuayene
        public virtual ICollection<KanTestiMuayene> KanTestleri { get; set; }

        //Stok Hareket tablosu ile 1-n ilişki 
        //Bir Muayenede birden fazla stok hareket olabilir
        public virtual ICollection<StokHareket>? StokHareketleri { get; set; } 

    }
}
