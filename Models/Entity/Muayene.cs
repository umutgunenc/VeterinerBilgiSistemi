﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Entity
{
    public partial class Muayene
    {
        public Muayene()
        {
            Stoklar = new HashSet<Stok>();
            Hastaliklar = new HashSet<HastalikMuayene>();
        }
        public int MuayeneId { get; set; }
        public int TedaviId { get; set; }
        public int HayvanId { get; set; }
        public DateTime MuayeneTarihi { get; set; }
        public DateTime? SonrakiMuayeneTarihi { get; set; }
        public string Aciklama { get; set; }
        public int HekimId { get; set; }
        public int StokId { get; set; }
        public virtual Hayvan Hayvan { get; set; }
        public virtual AppUser Hekim { get; set; }
        public virtual ICollection<Stok> Stoklar { get; set; }
        public virtual ICollection<HastalikMuayene> Hastaliklar { get; set; }
    }
}