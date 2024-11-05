using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hakkimizda",
                columns: table => new
                {
                    HakkimizdaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hakkimizda", x => x.HakkimizdaId);
                });

            migrationBuilder.CreateTable(
                name: "IletisimBilgileri",
                columns: table => new
                {
                    IletisimBilgileriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    Sehir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mahalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cadde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sokak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IletisimBilgileri", x => x.IletisimBilgileriId);
                });

            migrationBuilder.CreateTable(
                name: "SosyalMedyalar",
                columns: table => new
                {
                    SosyalMedyaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SosyalMedyaAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SosyalMedyaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SosyalMedyaPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosyalMedyalar", x => x.SosyalMedyaId);
                });

            migrationBuilder.CreateTable(
                name: "IletisimBilgileriSosyalMedyalar",
                columns: table => new
                {
                    IletisimBilgileriId = table.Column<int>(type: "int", nullable: false),
                    SosyalMedyaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IletisimBilgileriSosyalMedyalar", x => new { x.IletisimBilgileriId, x.SosyalMedyaId });
                    table.ForeignKey(
                        name: "FK_IletisimBilgileriSosyalMedyalar_IletisimBilgileri_IletisimBilgileriId",
                        column: x => x.IletisimBilgileriId,
                        principalTable: "IletisimBilgileri",
                        principalColumn: "IletisimBilgileriId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IletisimBilgileriSosyalMedyalar_SosyalMedyalar_SosyalMedyaId",
                        column: x => x.SosyalMedyaId,
                        principalTable: "SosyalMedyalar",
                        principalColumn: "SosyalMedyaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fb8cec90-0fea-4ab7-a603-65a2bacc51b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "322c665b-7bfb-4fac-bfba-aed4e0528bd0", "AQAAAAEAACcQAAAAEJfdq6xFVFhDu4kCbf5J3FZdZiMyYQn2U7qYNX8fV2BFzSSh+1HseiPtvg9gk/B7Bw==", "a4a11f0d-dc3f-4930-9a26-982211d0fdb7", new DateTime(3023, 11, 4, 11, 29, 32, 75, DateTimeKind.Local).AddTicks(7798), new DateTime(2024, 11, 4, 11, 29, 32, 74, DateTimeKind.Local).AddTicks(1552) });

            migrationBuilder.CreateIndex(
                name: "IX_IletisimBilgileriSosyalMedyalar_SosyalMedyaId",
                table: "IletisimBilgileriSosyalMedyalar",
                column: "SosyalMedyaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hakkimizda");

            migrationBuilder.DropTable(
                name: "IletisimBilgileriSosyalMedyalar");

            migrationBuilder.DropTable(
                name: "IletisimBilgileri");

            migrationBuilder.DropTable(
                name: "SosyalMedyalar");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c9ad05e7-1557-4430-859c-a487463aca05");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "550d3f72-b289-4446-82a2-134cbbf23bb5", "AQAAAAEAACcQAAAAECsh+qOUig12PIz0O5ZJeXdkiEChkUYlBzO5hQodFrVMX55xpuxjAlRL2hgdHwNOhg==", "8f587d09-5a43-4cd7-a6b2-5228cedcdf30", new DateTime(3023, 10, 21, 14, 7, 9, 440, DateTimeKind.Local).AddTicks(4935), new DateTime(2024, 10, 21, 14, 7, 9, 439, DateTimeKind.Local).AddTicks(2332) });
        }
    }
}
