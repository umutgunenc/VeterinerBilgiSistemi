﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinerBilgiSistemi.Data;

namespace VeterinerBilgiSistemi.Migrations
{
    [DbContext(typeof(VeterinerDBContext))]
    [Migration("20241008115911_mig3")]
    partial class mig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MuayeneStok", b =>
                {
                    b.Property<int>("MuayenelerMuayeneId")
                        .HasColumnType("int");

                    b.Property<int>("StoklarId")
                        .HasColumnType("int");

                    b.HasKey("MuayenelerMuayeneId", "StoklarId");

                    b.HasIndex("StoklarId");

                    b.ToTable("MuayeneStok");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "9c6db854-79d3-4ec1-8d9d-9d190637d19c",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("CalisiyorMu")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiplomaNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanSoyadi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsanTckn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SifreGecerlilikTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SifreOlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TermOfUse")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            CalisiyorMu = true,
                            ConcurrencyStamp = "70e8b689-d4c1-44ee-bd75-abecb0c67f76",
                            Email = "umutgunenc@gmail.com",
                            EmailConfirmed = false,
                            InsanAdi = "Umut",
                            InsanSoyadi = "Günenç",
                            InsanTckn = "24413435860",
                            LockoutEnabled = false,
                            NormalizedEmail = "UMUTGUNENC@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEAG1nElE+tCCr/3UDc/KuigGr71VsVmcA5MMx3Ee3nI2YSM7G9Gef6XuSox16n5mJA==",
                            PhoneNumber = "05300000000",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d5193fc1-202c-48b9-950f-3b77e77b70b4",
                            SifreGecerlilikTarihi = new DateTime(3023, 10, 8, 14, 59, 10, 201, DateTimeKind.Local).AddTicks(5782),
                            SifreOlusturmaTarihi = new DateTime(2024, 10, 8, 14, 59, 10, 199, DateTimeKind.Local).AddTicks(7350),
                            TermOfUse = true,
                            TwoFactorEnabled = false,
                            UserName = "ADMIN"
                        });
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Birim", b =>
                {
                    b.Property<int>("BirimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BirimAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BirimId");

                    b.ToTable("Birimler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Cins", b =>
                {
                    b.Property<int>("CinsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CinsAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CinsId");

                    b.ToTable("Cinsler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.CinsTur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CinsId")
                        .HasColumnType("int");

                    b.Property<int>("TurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinsId");

                    b.HasIndex("TurId");

                    b.ToTable("CinsTur");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Hastalik", b =>
                {
                    b.Property<int>("HastalikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("HastalikAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HastalikId");

                    b.ToTable("Hastalik");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.HastalikMuayene", b =>
                {
                    b.Property<int>("MuayeneId")
                        .HasColumnType("int")
                        .HasColumnName("MuayeneId");

                    b.Property<int>("HastalikId")
                        .HasColumnType("int")
                        .HasColumnName("HastalikId");

                    b.HasKey("MuayeneId", "HastalikId");

                    b.HasIndex("HastalikId");

                    b.ToTable("HastalikMuayene");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Hayvan", b =>
                {
                    b.Property<int>("HayvanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CinsTurId")
                        .HasColumnType("int");

                    b.Property<string>("HayvanAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HayvanAnneId")
                        .HasColumnType("int");

                    b.Property<int?>("HayvanBabaId")
                        .HasColumnType("int");

                    b.Property<int>("HayvanCinsiyet")
                        .HasColumnType("int");

                    b.Property<DateTime>("HayvanDogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double>("HayvanKilo")
                        .HasColumnType("float");

                    b.Property<DateTime?>("HayvanOlumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RenkId")
                        .HasColumnType("int");

                    b.HasKey("HayvanId");

                    b.HasIndex("CinsTurId");

                    b.HasIndex("HayvanAnneId");

                    b.HasIndex("HayvanBabaId");

                    b.HasIndex("RenkId");

                    b.ToTable("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.KanDegerleri", b =>
                {
                    b.Property<int>("KanDegerleriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double?>("ALT")
                        .HasColumnType("float");

                    b.Property<double?>("AST")
                        .HasColumnType("float");

                    b.Property<double?>("Albumin")
                        .HasColumnType("float");

                    b.Property<double?>("EritrositSayisi")
                        .HasColumnType("float");

                    b.Property<double?>("Glikoz")
                        .HasColumnType("float");

                    b.Property<double?>("Hematokrit")
                        .HasColumnType("float");

                    b.Property<double?>("Hemoglobin")
                        .HasColumnType("float");

                    b.Property<double?>("KarbondioksitDoygunlugu")
                        .HasColumnType("float");

                    b.Property<double?>("Klorür")
                        .HasColumnType("float");

                    b.Property<double?>("Kreatinin")
                        .HasColumnType("float");

                    b.Property<double?>("LökositSayisi")
                        .HasColumnType("float");

                    b.Property<int>("MuayeneId")
                        .HasColumnType("int");

                    b.Property<double?>("OksijenDoygunlugu")
                        .HasColumnType("float");

                    b.Property<double?>("Potasyum")
                        .HasColumnType("float");

                    b.Property<double?>("Sodyum")
                        .HasColumnType("float");

                    b.Property<double?>("TrombositSayisi")
                        .HasColumnType("float");

                    b.Property<double?>("Ure")
                        .HasColumnType("float");

                    b.HasKey("KanDegerleriId");

                    b.HasIndex("MuayeneId")
                        .IsUnique();

                    b.ToTable("KanDegerleri");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Kategori", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool?>("IlacMi")
                        .HasColumnType("bit");

                    b.Property<string>("KategoriAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategoriId");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Muayene", b =>
                {
                    b.Property<int>("MuayeneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HayvanId")
                        .HasColumnType("int");

                    b.Property<int>("HekimId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MuayeneTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SonrakiMuayeneTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokId")
                        .HasColumnType("int");

                    b.Property<int>("TedaviId")
                        .HasColumnType("int");

                    b.HasKey("MuayeneId");

                    b.HasIndex("HayvanId");

                    b.HasIndex("HekimId");

                    b.ToTable("Muayeneler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Renk", b =>
                {
                    b.Property<int>("RenkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RenkAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RenkId");

                    b.ToTable("Renkler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.SahipHayvan", b =>
                {
                    b.Property<int>("SahipId")
                        .HasColumnType("int")
                        .HasColumnName("SahiplerId");

                    b.Property<int>("HayvanId")
                        .HasColumnType("int")
                        .HasColumnName("HayvanId");

                    b.Property<DateTime?>("SahiplenmeCikisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SahiplenmeTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("SahipId", "HayvanId");

                    b.HasIndex("HayvanId");

                    b.ToTable("SahipHayvan");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Stok", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AktifMi")
                        .HasColumnType("bit");

                    b.Property<int>("BirimId")
                        .HasColumnType("int");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("StokAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StokBarkod")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BirimId");

                    b.HasIndex("KategoriId");

                    b.ToTable("Stoklar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.StokHareket", b =>
                {
                    b.Property<int>("StokHareketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("AlisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SatisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<double?>("StokCikisAdet")
                        .HasColumnType("float");

                    b.Property<double?>("StokGirisAdet")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StokHareketTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokId")
                        .HasColumnType("int");

                    b.HasKey("StokHareketId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("StokId");

                    b.ToTable("StokHareketler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Tur", b =>
                {
                    b.Property<int>("TurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TurAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TurId");

                    b.ToTable("Turler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.UserFace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("FaceData")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFaces");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MuayeneStok", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Muayene", null)
                        .WithMany()
                        .HasForeignKey("MuayenelerMuayeneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Stok", null)
                        .WithMany()
                        .HasForeignKey("StoklarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.CinsTur", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Cins", "Cins")
                        .WithMany("Turler")
                        .HasForeignKey("CinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Tur", "Tur")
                        .WithMany("Cinsler")
                        .HasForeignKey("TurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cins");

                    b.Navigation("Tur");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.HastalikMuayene", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Hastalik", "Hastalik")
                        .WithMany("Muayeneler")
                        .HasForeignKey("HastalikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Muayene", "Muayene")
                        .WithMany("Hastaliklar")
                        .HasForeignKey("MuayeneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hastalik");

                    b.Navigation("Muayene");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Hayvan", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.CinsTur", "CinsTur")
                        .WithMany("Hayvanlar")
                        .HasForeignKey("CinsTurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Hayvan", "HayvanAnne")
                        .WithMany("AnneninCocuklari")
                        .HasForeignKey("HayvanAnneId")
                        .HasConstraintName("FK__Hayvan__Anne");

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Hayvan", "HayvanBaba")
                        .WithMany("BabaninCocuklari")
                        .HasForeignKey("HayvanBabaId")
                        .HasConstraintName("FK__Hayvan__Baba");

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Renk", "Renk")
                        .WithMany("Hayvanlar")
                        .HasForeignKey("RenkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinsTur");

                    b.Navigation("HayvanAnne");

                    b.Navigation("HayvanBaba");

                    b.Navigation("Renk");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.KanDegerleri", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Muayene", "Muayene")
                        .WithOne("KanDegerleri")
                        .HasForeignKey("VeterinerBilgiSistemi.Models.Entity.KanDegerleri", "MuayeneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Muayene");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Muayene", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Hayvan", "Hayvan")
                        .WithMany("Muayeneler")
                        .HasForeignKey("HayvanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", "Hekim")
                        .WithMany("Muayeneler")
                        .HasForeignKey("HekimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hayvan");

                    b.Navigation("Hekim");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.SahipHayvan", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Hayvan", "Hayvan")
                        .WithMany("Sahipler")
                        .HasForeignKey("HayvanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", "AppUser")
                        .WithMany("Hayvanlar")
                        .HasForeignKey("SahipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Hayvan");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Stok", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Birim", "Birim")
                        .WithMany("Stoklar")
                        .HasForeignKey("BirimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Kategori", "Kategori")
                        .WithMany("Stoklar")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birim");

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.StokHareket", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", "Calisan")
                        .WithMany()
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.Stok", "Stok")
                        .WithMany("StokHareketleri")
                        .HasForeignKey("StokId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Stok");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.UserFace", b =>
                {
                    b.HasOne("VeterinerBilgiSistemi.Models.Entity.AppUser", "User")
                        .WithMany("UserFaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.AppUser", b =>
                {
                    b.Navigation("Hayvanlar");

                    b.Navigation("Muayeneler");

                    b.Navigation("UserFaces");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Birim", b =>
                {
                    b.Navigation("Stoklar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Cins", b =>
                {
                    b.Navigation("Turler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.CinsTur", b =>
                {
                    b.Navigation("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Hastalik", b =>
                {
                    b.Navigation("Muayeneler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Hayvan", b =>
                {
                    b.Navigation("AnneninCocuklari");

                    b.Navigation("BabaninCocuklari");

                    b.Navigation("Muayeneler");

                    b.Navigation("Sahipler");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Kategori", b =>
                {
                    b.Navigation("Stoklar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Muayene", b =>
                {
                    b.Navigation("Hastaliklar");

                    b.Navigation("KanDegerleri");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Renk", b =>
                {
                    b.Navigation("Hayvanlar");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Stok", b =>
                {
                    b.Navigation("StokHareketleri");
                });

            modelBuilder.Entity("VeterinerBilgiSistemi.Models.Entity.Tur", b =>
                {
                    b.Navigation("Cinsler");
                });
#pragma warning restore 612, 618
        }
    }
}
