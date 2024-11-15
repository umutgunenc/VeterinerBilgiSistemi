using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class KanTestiMuayene
    {
        [ForeignKey(nameof(MuayeneId))]
        public int MuayeneId { get; set; }
        public virtual Muayene Muayene { get; set; }

        [ForeignKey(nameof(KanDegerleriId))]
        public int KanDegerleriId { get; set; }
        public virtual KanDegerleri KanDegerleri { get; set; }
        public double? KanDegeriValue { get; set; }

    }
}
