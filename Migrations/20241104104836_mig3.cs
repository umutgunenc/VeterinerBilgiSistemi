using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnaSayfaFotograflar",
                columns: table => new
                {
                    AnaSayfaFotograflarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotografAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaSayfaFotograflar", x => x.AnaSayfaFotograflarId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "44e36468-10e8-482f-b2c4-2bafbb5644c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "57740a1f-15a6-49b7-bfdb-f2c034c1b269", "AQAAAAEAACcQAAAAEMI21MLu74wotJ3GB1q/Xftx5JvS/7y9OfmdeED3khONbqayWHGn9DxLlrX5x0hRzg==", "83208dc8-bedc-481d-a82c-3a80f51f8aaf", new DateTime(3023, 11, 4, 13, 48, 35, 554, DateTimeKind.Local).AddTicks(5794), new DateTime(2024, 11, 4, 13, 48, 35, 553, DateTimeKind.Local).AddTicks(8160) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaSayfaFotograflar");

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
        }
    }
}
