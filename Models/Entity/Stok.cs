using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Stok
    {
        public Stok()
        {
            StokHareketleri = new HashSet<StokHareket>();
        }
        public int Id { get; set; }
        public string StokBarkod { get; set; }
        public string StokAdi { get; set; }
        public int BirimId { get; set; }
        public string Aciklama { get; set; }
        public bool AktifMi { get; set; }       
        public int KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Birim Birim { get; set; }

        public virtual ICollection<StokHareket> StokHareketleri { get; set; }
    }
}
