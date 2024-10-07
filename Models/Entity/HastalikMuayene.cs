using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VeterinerBilgiSistemi.Models.Entity
{
    public class HastalikMuayene
    {

            [ForeignKey(nameof(MuayeneId))]
            public int MuayeneId { get; set; }
            public virtual Muayene Muayene { get; set; }

            [ForeignKey(nameof(HastalikId))]
            public int HastalikId { get; set; }
            public virtual Hastalik Hastalik { get; set; }
       
    }
}
