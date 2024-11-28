using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0afc032d-bc9e-4c39-9b45-1247ee05209d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "eea0e32a-ecf6-42b7-8646-d373d97faea5", "AQAAAAEAACcQAAAAEEqBcMP46H6zbOirzvUpUu8KA7S0U7gUBidDRDQLos7ZHDtZ4IyRA5lXnqE+zGnCqg==", "8bee38d2-6719-4c42-8a6f-602125198468", new DateTime(3023, 11, 26, 0, 48, 9, 837, DateTimeKind.Local).AddTicks(4093), new DateTime(2024, 11, 26, 0, 48, 9, 836, DateTimeKind.Local).AddTicks(4488) });

            migrationBuilder.InsertData(
                table: "Hastalik",
                columns: new[] { "HastalikId", "HastalikAdi" },
                values: new object[,]
                {
                    { 1, "Sağlıklı" },
                    { 2, "Böbrek Yetmezliği" },
                    { 3, "Anemi" },
                    { 4, "Polisitemi" },
                    { 5, "Akut Enfeksiyon" },
                    { 6, "Lösemi" },
                    { 7, "Karaciğer Hastalıkları" },
                    { 8, "Diyabet" },
                    { 9, "Hipoalbuminemi" },
                    { 10, "Hiponatremi" },
                    { 11, "Hiperkalemi" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hastalik",
                keyColumn: "HastalikId",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "99d9b061-c0b1-46c2-90b3-3564f582311c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "97ee3ce6-6ff6-47e4-88c3-6a3161005e29", "AQAAAAEAACcQAAAAELDkUP0oq+09rAreAz2NoGPwV8EC7xTU1BhnhCZg1XssdwPvOk4+IRHJ+r+a6lQb4g==", "d9f61211-902d-4fc8-8eda-3c5dd17ae074", new DateTime(3023, 11, 14, 23, 52, 34, 421, DateTimeKind.Local).AddTicks(8401), new DateTime(2024, 11, 14, 23, 52, 34, 419, DateTimeKind.Local).AddTicks(7772) });
        }
    }
}
