using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TedaviId",
                table: "Muayeneler");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2023ddce-b81a-4957-9637-3e6ce4b2127b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "a8082c66-b77a-40a2-b1d4-8dee5f2eec6b", "AQAAAAEAACcQAAAAEIYSQMdx2y9gcYIUMwB8YSSHWg4V75VprJ3cuUOI6k1vF3lUHy96mVfz9KL0u+OwXw==", "925f0a3b-7030-4705-a8ba-6b3163245d6a", new DateTime(3023, 12, 6, 14, 38, 18, 918, DateTimeKind.Local).AddTicks(3310), new DateTime(2024, 12, 6, 14, 38, 18, 917, DateTimeKind.Local).AddTicks(5039) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TedaviId",
                table: "Muayeneler",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
