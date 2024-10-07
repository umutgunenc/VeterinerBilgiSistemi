using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IlacMi",
                table: "Kategoriler",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ccef3b37-285c-41b4-a678-9b55599f97c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "20db0f33-f65c-4e99-930c-528f5a94daa4", "AQAAAAEAACcQAAAAEB9grCImGflXJdpCEpfK4E73M8nn63K5TLnHI8PEKLEDJfdx08aRnPEoWZwFw7nLvQ==", "f05c214f-4943-42d5-9a79-cde583e86722", new DateTime(3023, 10, 7, 13, 58, 3, 633, DateTimeKind.Local).AddTicks(8569), new DateTime(2024, 10, 7, 13, 58, 3, 632, DateTimeKind.Local).AddTicks(7452) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IlacMi",
                table: "Kategoriler",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "66bdefd7-69bb-469b-a373-c2cd2be93b96");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "3f012a85-67a4-457a-aeac-3f8a71032dde", "AQAAAAEAACcQAAAAELC1vSvQzR0H7C+958YKpmOs+FurXoK/kghyGIpBNNyjrJVbHNdDVkGGHVUa9ssoqg==", "2cf785bd-659d-42ff-9b5e-5192ad5f7a16", new DateTime(3023, 10, 7, 0, 31, 48, 95, DateTimeKind.Local).AddTicks(9630), new DateTime(2024, 10, 7, 0, 31, 48, 94, DateTimeKind.Local).AddTicks(9664) });
        }
    }
}
