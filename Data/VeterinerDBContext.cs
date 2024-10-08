using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Data
{
    public class VeterinerDBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IConfiguration _configuration;
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

        //Ara tablolar
        public virtual DbSet<CinsTur> CinsTur { get; set; }
        public virtual DbSet<SahipHayvan> SahipHayvan { get; set; }
        public virtual DbSet<HastalikMuayene> HastalikMuayene { get; set; }



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



            modelBuilder.Entity<HastalikMuayene>()
                .HasKey(hm => new { hm.MuayeneId, hm.HastalikId });

            modelBuilder.Entity<HastalikMuayene>()
                .HasOne(hm => hm.Hastalik)
                .WithMany(m => m.Muayeneler)
                .HasForeignKey(hm => hm.HastalikId);

            modelBuilder.Entity<HastalikMuayene>()
                .HasOne(hm => hm.Muayene)
                .WithMany(m => m.Hastaliklar)
                .HasForeignKey(hm => hm.MuayeneId);

            modelBuilder.Entity<HastalikMuayene>()
                .Property(hm => hm.MuayeneId)
                .HasColumnName("MuayeneId");

            modelBuilder.Entity<HastalikMuayene>()
                .Property(hm => hm.HastalikId)
                .HasColumnName("HastalikId");


            modelBuilder.Entity<AppRole>()
                .HasData(new AppRole { Id = 1, Name = "ADMIN", NormalizedName = "ADMIN" });

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

            };
            var passwordHasher = new PasswordHasher<AppUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123123123");
            admin.SecurityStamp = Guid.NewGuid().ToString();
            modelBuilder.Entity<AppUser>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );

            base.OnModelCreating(modelBuilder);

        }

    }
}
