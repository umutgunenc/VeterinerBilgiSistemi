using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Hastalik
    {
        public Hastalik()
        {
            Muayeneler = new HashSet<HastalikMuayene>();

        }
        public int HastalikId { get; set; }
        public string HastalikAdi { get; set; }

        public virtual ICollection<HastalikMuayene> Muayeneler { get; set; }
    }
}
