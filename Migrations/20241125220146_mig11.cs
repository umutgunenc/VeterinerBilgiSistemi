using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cb4c98ba-ec4c-464b-b178-6a8c63e66341");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "73617997-cd32-44b4-8a92-6652389f6a61", "AQAAAAEAACcQAAAAEJHp/y70dJ3S43Kdg2iwKhSkmpARVC1MvIHdVAn/F8koB6fihz7KoyzuPacXAySGxQ==", "30b8bcb4-848b-4343-b221-229c10e8c819", new DateTime(3023, 11, 26, 1, 1, 45, 899, DateTimeKind.Local).AddTicks(8506), new DateTime(2024, 11, 26, 1, 1, 45, 898, DateTimeKind.Local).AddTicks(6257) });

            migrationBuilder.InsertData(
                table: "KanDegerleri",
                columns: new[] { "KanDegerleriId", "AktifMi", "AltLimit", "KanTestiAdi", "KanTestiBirimi", "UstLimit" },
                values: new object[,]
                {
                    { 15, true, 95.0, "Oksijen Doygunluğu - SpO₂", "%", 100.0 },
                    { 14, true, 100.0, "Klorür", "mEq/L", 118.0 },
                    { 13, true, 3.5, "Potasyum", "mEq/L", 5.8 },
                    { 12, true, 138.0, "Sodyum", "mEq/L", 154.0 },
                    { 11, true, 75.0, "Glikoz", "mg/dL", 120.0 },
                    { 10, true, 2.6, "Albumin", "g/dL", 4.0 },
                    { 9, true, 7.0, "Üre", "mg/dL", 27.0 },
                    { 7, true, 10.0, "Alanin - ALT", "U/L", 109.0 },
                    { 6, true, 160.0, "Trombosit Sayısı", "bin/µL", 430.0 },
                    { 5, true, 6.0, "Lökosit Sayısı", "bin/µL", 17.0 },
                    { 4, true, 5.5, "Eritrosit Sayısı", "milyon/µL", 8.5 },
                    { 3, true, 37.0, "Hematokrit Yüzdesi", "%", 55.0 },
                    { 2, true, 12.0, "Hemoglobin", "g/dL", 18.0 },
                    { 8, true, 15.0, "Aspartat - AST", "U/L", 66.0 },
                    { 1, true, 0.4, "Kreatinin", "mg/dL", 1.8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "KanDegerleri",
                keyColumn: "KanDegerleriId",
                keyValue: 15);

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
        }
    }
}
