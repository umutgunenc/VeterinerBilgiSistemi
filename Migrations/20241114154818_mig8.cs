using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HastalikMuayene");

            migrationBuilder.AddColumn<int>(
                name: "HastalikId",
                table: "Muayeneler",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Muayeneler_HastalikId",
                table: "Muayeneler",
                column: "HastalikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler",
                column: "HastalikId",
                principalTable: "Hastalik",
                principalColumn: "HastalikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muayeneler_Hastalik_HastalikId",
                table: "Muayeneler");

            migrationBuilder.DropIndex(
                name: "IX_Muayeneler_HastalikId",
                table: "Muayeneler");

            migrationBuilder.DropColumn(
                name: "HastalikId",
                table: "Muayeneler");

            migrationBuilder.CreateTable(
                name: "HastalikMuayene",
                columns: table => new
                {
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    HastalikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HastalikMuayene", x => new { x.MuayeneId, x.HastalikId });
                    table.ForeignKey(
                        name: "FK_HastalikMuayene_Hastalik_HastalikId",
                        column: x => x.HastalikId,
                        principalTable: "Hastalik",
                        principalColumn: "HastalikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HastalikMuayene_Muayeneler_MuayeneId",
                        column: x => x.MuayeneId,
                        principalTable: "Muayeneler",
                        principalColumn: "MuayeneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "29d914a7-9caa-49ca-8176-2e3c1a0a1022");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "1aad2938-67d8-487f-8e00-a67671990d25", "AQAAAAEAACcQAAAAEBsNVGoc47jxiLp5tMyq9meFMKy9FOBeiaDyUyVL+Vkng4rl/zs1uQC69vNNWuGcWQ==", "ccf34f7d-bde9-472e-a503-af26983fe3f6", new DateTime(3023, 11, 14, 16, 16, 21, 906, DateTimeKind.Local).AddTicks(1863), new DateTime(2024, 11, 14, 16, 16, 21, 904, DateTimeKind.Local).AddTicks(9659) });

            migrationBuilder.CreateIndex(
                name: "IX_HastalikMuayene_HastalikId",
                table: "HastalikMuayene",
                column: "HastalikId");
        }
    }
}
