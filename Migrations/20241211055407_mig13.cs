using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler");

            migrationBuilder.AddColumn<int>(
                name: "YapayZekaTahminId",
                table: "Muayeneler",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "438c42ad-464c-4bb1-b064-0959259931de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "2bb24191-2414-4ffb-a396-05481b772864", "AQAAAAEAACcQAAAAEJb9Us5+c0oSt2HHmMXb+smYnVYgqCSKe6aHaL1KPxzlFstbPlnxAJxeXmuhFpwtog==", "2832b7db-253b-484e-ba86-b09b9c474c4a", new DateTime(3023, 12, 11, 8, 54, 6, 478, DateTimeKind.Local).AddTicks(5854), new DateTime(2024, 12, 11, 8, 54, 6, 477, DateTimeKind.Local).AddTicks(5269) });

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_YapayZekaTahminId",
                table: "Muayeneler",
                column: "YapayZekaTahminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler",
                column: "HastalikId",
                principalTable: "Hastalik",
                principalColumn: "HastalikId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Muayeneler_Hastalik_YapayZekaTahminId",
                table: "Muayeneler",
                column: "YapayZekaTahminId",
                principalTable: "Hastalik",
                principalColumn: "HastalikId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler");

            migrationBuilder.DropForeignKey(
                name: "FK_Muayeneler_Hastalik_YapayZekaTahminId",
                table: "Muayeneler");

            migrationBuilder.DropIndex(
                name: "IX_Muayeneler_YapayZekaTahminId",
                table: "Muayeneler");

            migrationBuilder.DropColumn(
                name: "YapayZekaTahminId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler",
                column: "HastalikId",
                principalTable: "Hastalik",
                principalColumn: "HastalikId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
