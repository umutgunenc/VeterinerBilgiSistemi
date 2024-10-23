using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class StokMuayene
    {
        [ForeignKey(nameof(MuayeneId))]
        public int MuayeneId { get; set; }
        public virtual Muayene Muayene { get; set; }

        [ForeignKey(nameof(StokId))]
        public int StokId { get; set; }
        public virtual Stok Stok { get; set; }

    }
}
