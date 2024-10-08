using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "SahipHayvan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7eff5d89-35da-40f1-acff-430dd700acee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "0ed6ece0-3095-4440-9e2a-71dd3b61c901", "AQAAAAEAACcQAAAAEA5cnxf8l9V6WBdhNDGqJBP+r7gErIuWaJgLPQ4Bkxsou6qMorOTq9i2dfl70wPNgw==", "2ad185db-4777-404d-82de-3d4a13177692", new DateTime(3023, 10, 8, 16, 11, 29, 77, DateTimeKind.Local).AddTicks(8315), new DateTime(2024, 10, 8, 16, 11, 29, 75, DateTimeKind.Local).AddTicks(8708) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "SahipHayvan");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9c6db854-79d3-4ec1-8d9d-9d190637d19c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "70e8b689-d4c1-44ee-bd75-abecb0c67f76", "AQAAAAEAACcQAAAAEAG1nElE+tCCr/3UDc/KuigGr71VsVmcA5MMx3Ee3nI2YSM7G9Gef6XuSox16n5mJA==", "d5193fc1-202c-48b9-950f-3b77e77b70b4", new DateTime(3023, 10, 8, 14, 59, 10, 201, DateTimeKind.Local).AddTicks(5782), new DateTime(2024, 10, 8, 14, 59, 10, 199, DateTimeKind.Local).AddTicks(7350) });
        }
    }
}
