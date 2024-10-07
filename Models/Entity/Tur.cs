using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Tur
    {
        public Tur()
        {
            Cinsler = new HashSet<CinsTur>();
        }
        public int TurId { get; set; }
        public string TurAdi { get; set; }

        public virtual ICollection<CinsTur> Cinsler { get; set; }
    }
}
