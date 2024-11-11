using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "KanDegeriValue",
                table: "KanTestiMuayene",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7fec95cb-d96f-42c9-aded-e3f1b7040673");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "c14b19d5-7164-44f0-be74-dbb2b446fb43", "AQAAAAEAACcQAAAAEAIbJSq7+aaycS7MtuJ2louIM32pvfaCOSblkwJJwr0E0EXEnHLc+3U3wl7Cx5BTLQ==", "28bf0a22-7dcb-49ef-9a2c-b7fe00431181", new DateTime(3023, 11, 11, 13, 0, 6, 833, DateTimeKind.Local).AddTicks(8746), new DateTime(2024, 11, 11, 13, 0, 6, 832, DateTimeKind.Local).AddTicks(3299) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KanDegeriValue",
                table: "KanTestiMuayene");

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
    }
}
