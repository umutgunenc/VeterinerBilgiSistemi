using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StokMuayane");

            migrationBuilder.AddColumn<int>(
                name: "MuayeneId",
                table: "StokHareketler",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketler_MuayeneId",
                table: "StokHareketler",
                column: "MuayeneId");

            migrationBuilder.AddForeignKey(
                name: "FK_StokHareketler_Muayeneler_MuayeneId",
                table: "StokHareketler",
                column: "MuayeneId",
                principalTable: "Muayeneler",
                principalColumn: "MuayeneId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StokHareketler_Muayeneler_MuayeneId",
                table: "StokHareketler");

            migrationBuilder.DropIndex(
                name: "IX_StokHareketler_MuayeneId",
                table: "StokHareketler");

            migrationBuilder.DropColumn(
                name: "MuayeneId",
                table: "StokHareketler");

            migrationBuilder.CreateTable(
                name: "StokMuayane",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokMuayane", x => new { x.MuayeneId, x.StokId });
                    table.ForeignKey(
                        name: "FK_StokMuayane_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokMuayane_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bc6747ac-9dd8-4b0c-862a-3c545bf44bd6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "be856c2a-ab66-46c2-b0b3-d6cad3c58ecf", "AQAAAAEAACcQAAAAEEnEg5OSHDCn9C4k0MtOORM8Fms9yDLfXWn6E9jwM5MdZsNp57xK3qTCHNB5Zypy1g==", "353265bf-788b-4716-a1c0-a30bcf9269c0", new DateTime(3023, 11, 14, 18, 48, 17, 885, DateTimeKind.Local).AddTicks(183), new DateTime(2024, 11, 14, 18, 48, 17, 883, DateTimeKind.Local).AddTicks(8797) });

            migrationBuilder.CreateIndex(
                name: "IX_StokMuayane_StokId",
                table: "StokMuayane",
                column: "StokId");
        }
    }
}
