using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Hastalik
    {
        public Hastalik()
        {
            Muayeneler = new HashSet<Muayene>();
        }
        public int HastalikId { get; set; }
        public string HastalikAdi { get; set; }

        public virtual ICollection<Muayene> Muayeneler { get; set; }
        public virtual ICollection<Muayene> YapayZekaTahminMuayeneleri { get; set; }

    }
}
