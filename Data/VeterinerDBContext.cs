using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Data
{
    public class VeterinerDBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public VeterinerDBContext()
        {

        }

        public VeterinerDBContext(DbContextOptions<VeterinerDBContext> options) : base(options)
        {

        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Birim> Birimler { get; set; }
        public virtual DbSet<Cins> Cinsler { get; set; }
        public virtual DbSet<Hayvan> Hayvanlar { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<Muayene> Muayeneler { get; set; }
        public virtual DbSet<Renk> Renkler { get; set; }
        public virtual DbSet<Stok> Stoklar { get; set; }
        public virtual DbSet<StokHareket> StokHareketler { get; set; }
        public virtual DbSet<Hastalik> Tedaviler { get; set; }
        public virtual DbSet<Tur> Turler { get; set; }
        public virtual DbSet<UserFace> UserFaces { get; set; }
        public virtual DbSet<Hastalik> Hastaliklar { get; set; }
        public virtual DbSet<KanDegerleri> KanDegerleri { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
        public virtual DbSet<IletisimBilgileri> IletisimBilgileri { get; set; }
        public virtual DbSet<SosyalMedya> SosyalMedyalar { get; set; }
        public virtual DbSet<AnaSayfaFotograflar> AnaSayfaFotograflar { get; set; }

        //Ara tablolar n-n
        public virtual DbSet<CinsTur> CinsTur { get; set; }
        public virtual DbSet<SahipHayvan> SahipHayvan { get; set; }
        public virtual DbSet<KanTestiMuayene> KanTestiMuayene { get; set; }
        public virtual DbSet<IletisimBilgileriSosyalMedya> IletisimBilgileriSosyalMedyalar { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5OQQMU6\\SQLEXPRESS;Database=VeterinerBilgiSistemi;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hayvan>(entity =>
            {
                entity.HasOne(e => e.HayvanAnne)
                .WithMany(p => p.AnneninCocuklari)
                .HasForeignKey(d => d.HayvanAnneId)
                .HasConstraintName("FK__Hayvan__Anne");

                entity.HasOne(e => e.HayvanBaba)
                .WithMany(p => p.BabaninCocuklari)
                .HasForeignKey(d => d.HayvanBabaId)
                .HasConstraintName("FK__Hayvan__Baba");

            });

            modelBuilder.Entity<SahipHayvan>()
                .HasKey(sh => new { sh.SahipId, sh.HayvanId });

            modelBuilder.Entity<SahipHayvan>()
                .HasOne(sh => sh.Hayvan)
                .WithMany(h => h.Sahipler)
                .HasForeignKey(sh => sh.HayvanId);

            modelBuilder.Entity<SahipHayvan>()
                .HasOne(sh => sh.AppUser)
                .WithMany(u => u.Hayvanlar)
                .HasForeignKey(sh => sh.SahipId);

            modelBuilder.Entity<SahipHayvan>()
                .Property(sh => sh.HayvanId)
                .HasColumnName("HayvanId");

            modelBuilder.Entity<SahipHayvan>()
                .Property(sh => sh.SahipId)
                .HasColumnName("SahiplerId");

            modelBuilder.Entity<SahipHayvan>()
                .Property(sh => sh.SahiplenmeTarihi)
                .IsRequired();




            modelBuilder.Entity<KanTestiMuayene>()
                .HasKey(km => new { km.MuayeneId, km.KanDegerleriId });

            modelBuilder.Entity<KanTestiMuayene>()
                .HasOne(km => km.KanDegerleri)
                .WithMany(m => m.Muayeneler)
                .HasForeignKey(km => km.KanDegerleriId);

            modelBuilder.Entity<KanTestiMuayene>()
                .HasOne(km => km.Muayene)
                .WithMany(k => k.KanTestleri)
                .HasForeignKey(km => km.MuayeneId);

            modelBuilder.Entity<KanTestiMuayene>()
                .Property(km => km.MuayeneId)
                .HasColumnName("MuayeneId");

            modelBuilder.Entity<KanTestiMuayene>()
                .Property(km => km.KanDegerleriId)
                .HasColumnName("KanDegerleriId");


            modelBuilder.Entity<IletisimBilgileriSosyalMedya>()
                .HasKey(ibsm => new { ibsm.IletisimBilgileriId, ibsm.SosyalMedyaId });

            modelBuilder.Entity<IletisimBilgileriSosyalMedya>()
                .HasOne(ibsm => ibsm.IletisimBilgileri)
                .WithMany(sm => sm.SosyalMedyalar)
                .HasForeignKey(ibsm => ibsm.IletisimBilgileriId);

            modelBuilder.Entity<IletisimBilgileriSosyalMedya>()
                .HasOne(ibsm => ibsm.SosyalMedya)
                .WithMany(ib => ib.IletisimBilgileri)
                .HasForeignKey(km => km.SosyalMedyaId);

            modelBuilder.Entity<IletisimBilgileriSosyalMedya>()
                .Property(ibsm => ibsm.IletisimBilgileriId)
                .HasColumnName("IletisimBilgileriId");

            modelBuilder.Entity<IletisimBilgileriSosyalMedya>()
                .Property(ibsm => ibsm.SosyalMedyaId)
                .HasColumnName("SosyalMedyaId");



            modelBuilder.Entity<Muayene>()
                .HasOne(m => m.Hastalik)
                .WithMany(h => h.Muayeneler)
                .HasForeignKey(m => m.HastalikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Muayene>()
                .HasOne(m => m.YapayZekaTahminHastaligi)
                .WithMany(m => m.YapayZekaTahminMuayeneleri)
                .HasForeignKey(m => m.YapayZekaTahminId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<AppRole>()
                .HasData(new AppRole { Id = 1, Name = "ADMIN", NormalizedName = "ADMIN" });


            var passwordHasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = 1,
                UserName = "ADMIN",
                Email = "umutgunenc@gmail.com",
                InsanAdi = "Umut",
                InsanSoyadi = "Günenç",
                InsanTckn = "24413435860",
                CalisiyorMu = true,
                SifreOlusturmaTarihi = System.DateTime.Now,
                SifreGecerlilikTarihi = System.DateTime.Now.AddYears(999),
                TermOfUse = true,
                PhoneNumber = "05300000000",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "UMUTGUNENC@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),

            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123123123");

            modelBuilder.Entity<AppUser>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );


            var hastaliklar = new List<Hastalik>
            {
                new Hastalik { HastalikAdi = "Sağlıklı", HastalikId = 1 },
                new Hastalik { HastalikAdi = "Böbrek Yetmezliği", HastalikId = 2 },
                new Hastalik { HastalikAdi = "Anemi", HastalikId = 3 },
                new Hastalik { HastalikAdi = "Polisitemi", HastalikId = 4 },
                new Hastalik { HastalikAdi = "Akut Enfeksiyon", HastalikId = 5 },
                new Hastalik { HastalikAdi = "Lösemi", HastalikId = 6 },
                new Hastalik { HastalikAdi = "Karaciğer Hastalıkları", HastalikId = 7 },
                new Hastalik { HastalikAdi = "Diyabet", HastalikId = 8 },
                new Hastalik { HastalikAdi = "Hipoalbuminemi", HastalikId = 9 },
                new Hastalik { HastalikAdi = "Hiponatremi", HastalikId = 10 },
                new Hastalik { HastalikAdi = "Hiperkalemi", HastalikId = 11 }

            }.AsEnumerable();

            var kanTestleri = new List<KanDegerleri>
            {
                new KanDegerleri {KanDegerleriId = 1, AktifMi = true, AltLimit = 0.4, UstLimit = 1.8, KanTestiAdi =     "Kreatinin", KanTestiBirimi = "mg/dL"},
                new KanDegerleri {KanDegerleriId = 2, AktifMi = true, AltLimit = 12, UstLimit = 18, KanTestiAdi =   "Hemoglobin", KanTestiBirimi = "g/dL"},
                new KanDegerleri {KanDegerleriId = 3, AktifMi = true, AltLimit = 37, UstLimit = 55, KanTestiAdi = "Hematokrit Yüzdesi", KanTestiBirimi = "%"},
                new KanDegerleri {KanDegerleriId = 4, AktifMi = true, AltLimit = 5.5, UstLimit = 8.5, KanTestiAdi = "Eritrosit Sayısı", KanTestiBirimi = "milyon/µL"},
                new KanDegerleri {KanDegerleriId = 5, AktifMi = true, AltLimit = 6, UstLimit = 17, KanTestiAdi = "Lökosit Sayısı", KanTestiBirimi = "bin/µL"},
                new KanDegerleri {KanDegerleriId = 6, AktifMi = true, AltLimit = 160, UstLimit = 430, KanTestiAdi = "Trombosit Sayısı", KanTestiBirimi = "bin/µL"},
                new KanDegerleri {KanDegerleriId = 7, AktifMi = true, AltLimit = 10, UstLimit = 109, KanTestiAdi = "Alanin - ALT", KanTestiBirimi = "U/L"},
                new KanDegerleri {KanDegerleriId = 8, AktifMi = true, AltLimit = 15, UstLimit = 66, KanTestiAdi = "Aspartat - AST", KanTestiBirimi = "U/L"},
                new KanDegerleri {KanDegerleriId = 9, AktifMi = true, AltLimit = 7, UstLimit = 27, KanTestiAdi = "Üre", KanTestiBirimi = "mg/dL"},
                new KanDegerleri {KanDegerleriId = 10, AktifMi = true, AltLimit = 2.6, UstLimit = 4, KanTestiAdi = "Albumin", KanTestiBirimi = "g/dL"},
                new KanDegerleri {KanDegerleriId = 11, AktifMi = true, AltLimit = 75, UstLimit = 120, KanTestiAdi = "Glikoz", KanTestiBirimi = "mg/dL"},
                new KanDegerleri {KanDegerleriId = 12, AktifMi = true, AltLimit = 138, UstLimit = 154, KanTestiAdi = "Sodyum", KanTestiBirimi = "mEq/L"},
                new KanDegerleri {KanDegerleriId = 13, AktifMi = true, AltLimit = 3.5, UstLimit = 5.8, KanTestiAdi =    "Potasyum", KanTestiBirimi = "mEq/L"},
                new KanDegerleri {KanDegerleriId = 14, AktifMi = true, AltLimit = 100, UstLimit = 118, KanTestiAdi = "Klorür", KanTestiBirimi = "mEq/L"},
                new KanDegerleri {KanDegerleriId = 15, AktifMi = true, AltLimit = 95, UstLimit = 100, KanTestiAdi = "Oksijen Doygunluğu - SpO₂", KanTestiBirimi = "%"}
            }.AsEnumerable();


            modelBuilder.Entity<Hastalik>().HasData(hastaliklar);

            modelBuilder.Entity<KanDegerleri>().HasData(kanTestleri);

            base.OnModelCreating(modelBuilder);

        }

    }
}
