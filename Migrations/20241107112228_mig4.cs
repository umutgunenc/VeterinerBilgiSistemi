using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ilce",
                table: "IletisimBilgileri",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6610b9ce-5bfc-423d-a4f9-6859249e63c7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "75df064d-59a5-42aa-9195-a8b3e832799a", "AQAAAAEAACcQAAAAEHRdlkzLzKfxVsBb61LVmHNeR01ld2SDyiBiOuv3PuTtJdj/niepqK+T6lqlRItoLA==", "809426db-bd26-4598-abc4-84102eac096b", new DateTime(3023, 11, 7, 14, 22, 27, 978, DateTimeKind.Local).AddTicks(5904), new DateTime(2024, 11, 7, 14, 22, 27, 975, DateTimeKind.Local).AddTicks(9672) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ilce",
                table: "IletisimBilgileri");

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
    }
}
