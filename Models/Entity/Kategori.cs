using System.Collections.Generic;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class Kategori
    {
        public Kategori()
        {
            Stoklar = new HashSet<Stok>();
        }
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public bool? IlacMi { get; set; }
        public virtual ICollection<Stok> Stoklar { get; set; }
    }
}
