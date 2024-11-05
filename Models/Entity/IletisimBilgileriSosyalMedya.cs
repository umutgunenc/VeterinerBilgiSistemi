using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class IletisimBilgileriSosyalMedya
    {
        [ForeignKey(nameof(IletisimBilgileriId))]
        public int IletisimBilgileriId { get; set; }
        public IletisimBilgileri IletisimBilgileri { get; set; }


        [ForeignKey(nameof(SosyalMedyaId))]
        public int SosyalMedyaId { get; set; }
        public SosyalMedya SosyalMedya { get; set; }
    }
}
