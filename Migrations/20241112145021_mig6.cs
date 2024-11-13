using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StokId",
                table: "Muayeneler");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b767c0fd-afa3-4e34-9ba8-31c4836e7d40");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "34b67051-9d4d-4549-907a-6faf4960a0d0", "AQAAAAEAACcQAAAAEHlxyQSsuZSTlXehDlHqm77NK/D22nIlNELqSoklwlAn4L22TDWHjI6Zs+r0g6F+cg==", "697824c2-769f-46ab-8525-60c8ae6e845a", new DateTime(3023, 11, 12, 17, 50, 20, 441, DateTimeKind.Local).AddTicks(6369), new DateTime(2024, 11, 12, 17, 50, 20, 440, DateTimeKind.Local).AddTicks(6226) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StokId",
                table: "Muayeneler",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
