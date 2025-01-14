﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsanTckn = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    InsanAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InsanSoyadi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifreOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SifreGecerlilikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiplomaNo = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    CalisiyorMu = table.Column<bool>(type: "bit", nullable: false),
                    TermOfUse = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Birimler",
                columns: table => new
                {
                    BirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birimler", x => x.BirimId);
                });

            migrationBuilder.CreateTable(
                name: "Cinsler",
                columns: table => new
                {
                    CinsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinsAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinsler", x => x.CinsId);
                });

            migrationBuilder.CreateTable(
                name: "Hastalik",
                columns: table => new
                {
                    HastalikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastalikAdi = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastalik", x => x.HastalikId);
                });

            migrationBuilder.CreateTable(
                name: "KanDegerleri",
                columns: table => new
                {
                    KanDegerleriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanTestiAdi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    KanTestiBirimi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AltLimit = table.Column<double>(type: "float", nullable: true),
                    UstLimit = table.Column<double>(type: "float", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanDegerleri", x => x.KanDegerleriId);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IlacMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Renkler",
                columns: table => new
                {
                    RenkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenkAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renkler", x => x.RenkId);
                });

            migrationBuilder.CreateTable(
                name: "Turler",
                columns: table => new
                {
                    TurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurAdi = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turler", x => x.TurId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FaceData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFaces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stoklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokBarkod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StokAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirimId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoklar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stoklar_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "BirimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stoklar_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CinsTur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinsId = table.Column<int>(type: "int", nullable: false),
                    TurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinsTur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinsTur_Cinsler_CinsId",
                        column: x => x.CinsId,
                        principalTable: "Cinsler",
                        principalColumn: "CinsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinsTur_Turler_TurId",
                        column: x => x.TurId,
                        principalTable: "Turler",
                        principalColumn: "TurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketler",
                columns: table => new
                {
                    StokHareketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StokHareketTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StokId = table.Column<int>(type: "int", nullable: false),
                    SatisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    StokGirisAdet = table.Column<double>(type: "float", nullable: true),
                    StokCikisAdet = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketler", x => x.StokHareketId);
                    table.ForeignKey(
                        name: "FK_StokHareketler_AspNetUsers_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketler_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hayvanlar",
                columns: table => new
                {
                    HayvanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HayvanAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HayvanCinsiyet = table.Column<int>(type: "int", nullable: false),
                    HayvanKilo = table.Column<double>(type: "float", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HayvanDogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HayvanOlumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenkId = table.Column<int>(type: "int", nullable: false),
                    CinsTurId = table.Column<int>(type: "int", nullable: false),
                    HayvanAnneId = table.Column<int>(type: "int", nullable: true),
                    HayvanBabaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hayvanlar", x => x.HayvanId);
                    table.ForeignKey(
                        name: "FK__Hayvan__Anne",
                        column: x => x.HayvanAnneId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Hayvan__Baba",
                        column: x => x.HayvanBabaId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hayvanlar_CinsTur_CinsTurId",
                        column: x => x.CinsTurId,
                        principalTable: "CinsTur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hayvanlar_Renkler_RenkId",
                        column: x => x.RenkId,
                        principalTable: "Renkler",
                        principalColumn: "RenkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Muayeneler",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedaviId = table.Column<int>(type: "int", nullable: false),
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    MuayeneTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonrakiMuayeneTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HekimId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muayeneler", x => x.MuayeneId);
                    table.ForeignKey(
                        name: "FK_Muayeneler_AspNetUsers_HekimId",
                        column: x => x.HekimId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Muayeneler_Hayvanlar_HayvanId",
                        column: x => x.HayvanId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SahipHayvan",
                columns: table => new
                {
                    HayvanId = table.Column<int>(type: "int", nullable: false),
                    SahiplerId = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    SahiplenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SahiplenmeCikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahipHayvan", x => new { x.SahiplerId, x.HayvanId });
                    table.ForeignKey(
                        name: "FK_SahipHayvan_AspNetUsers_SahiplerId",
                        column: x => x.SahiplerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahipHayvan_Hayvanlar_HayvanId",
                        column: x => x.HayvanId,
                        principalTable: "Hayvanlar",
                        principalColumn: "HayvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HastalikMuayene",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    HastalikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HastalikMuayene", x => new { x.MuayeneId, x.HastalikId });
                    table.ForeignKey(
                        name: "FK_HastalikMuayene_Hastalik_HastalikId",
                        column: x => x.HastalikId,
                        principalTable: "Hastalik",
                        principalColumn: "HastalikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HastalikMuayene_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanTestiMuayene",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    KanDegerleriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanTestiMuayene", x => new { x.MuayeneId, x.KanDegerleriId });
                    table.ForeignKey(
                        name: "FK_KanTestiMuayene_KanDegerleri_KanDegerleriId",
                        column: x => x.KanDegerleriId,
                        principalTable: "KanDegerleri",
                        principalColumn: "KanDegerleriId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KanTestiMuayene_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokMuayane",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokMuayane", x => new { x.MuayeneId, x.StokId });
                    table.ForeignKey(
                        name: "FK_StokMuayane_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokMuayane_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "c9ad05e7-1557-4430-859c-a487463aca05", "ADMIN", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CalisiyorMu", "ConcurrencyStamp", "DiplomaNo", "Email", "EmailConfirmed", "ImgURL", "InsanAdi", "InsanSoyadi", "InsanTckn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi", "TermOfUse", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, true, "550d3f72-b289-4446-82a2-134cbbf23bb5", null, "umutgunenc@gmail.com", false, null, "Umut", "Günenç", "24413435860", false, null, "UMUTGUNENC@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAECsh+qOUig12PIz0O5ZJeXdkiEChkUYlBzO5hQodFrVMX55xpuxjAlRL2hgdHwNOhg==", "05300000000", false, "8f587d09-5a43-4cd7-a6b2-5228cedcdf30", new DateTime(3023, 10, 21, 14, 7, 9, 440, DateTimeKind.Local).AddTicks(4935), new DateTime(2024, 10, 21, 14, 7, 9, 439, DateTimeKind.Local).AddTicks(2332), true, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CinsTur_CinsId",
                table: "CinsTur",
                column: "CinsId");

            migrationBuilder.CreateIndex(
                name: "IX_CinsTur_TurId",
                table: "CinsTur",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_HastalikMuayene_HastalikId",
                table: "HastalikMuayene",
                column: "HastalikId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_CinsTurId",
                table: "Hayvanlar",
                column: "CinsTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_HayvanAnneId",
                table: "Hayvanlar",
                column: "HayvanAnneId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_HayvanBabaId",
                table: "Hayvanlar",
                column: "HayvanBabaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hayvanlar_RenkId",
                table: "Hayvanlar",
                column: "RenkId");

            migrationBuilder.CreateIndex(
                name: "IX_KanTestiMuayene_KanDegerleriId",
                table: "KanTestiMuayene",
                column: "KanDegerleriId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_HayvanId",
                table: "Muayeneler",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_HekimId",
                table: "Muayeneler",
                column: "HekimId");

            migrationBuilder.CreateIndex(
                name: "IX_SahipHayvan_HayvanId",
                table: "SahipHayvan",
                column: "HayvanId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketler_CalisanId",
                table: "StokHareketler",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketler_StokId",
                table: "StokHareketler",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoklar_BirimId",
                table: "Stoklar",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoklar_KategoriId",
                table: "Stoklar",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_StokMuayane_StokId",
                table: "StokMuayane",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFaces_UserId",
                table: "UserFaces",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HastalikMuayene");

            migrationBuilder.DropTable(
                name: "KanTestiMuayene");

            migrationBuilder.DropTable(
                name: "SahipHayvan");

            migrationBuilder.DropTable(
                name: "StokHareketler");

            migrationBuilder.DropTable(
                name: "StokMuayane");

            migrationBuilder.DropTable(
                name: "UserFaces");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Hastalik");

            migrationBuilder.DropTable(
                name: "KanDegerleri");

            migrationBuilder.DropTable(
                name: "Muayeneler");

            migrationBuilder.DropTable(
                name: "Stoklar");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Hayvanlar");

            migrationBuilder.DropTable(
                name: "Birimler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "CinsTur");

            migrationBuilder.DropTable(
                name: "Renkler");

            migrationBuilder.DropTable(
                name: "Cinsler");

            migrationBuilder.DropTable(
                name: "Turler");
        }
    }
}
