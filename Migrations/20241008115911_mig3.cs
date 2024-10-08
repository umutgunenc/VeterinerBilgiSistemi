using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KanDegerleri",
                columns: table => new
                {
                    KanDegerleriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuayeneId = table.Column<int>(type: "int", nullable: false),
                    Hemoglobin = table.Column<double>(type: "float", nullable: true),
                    Hematokrit = table.Column<double>(type: "float", nullable: true),
                    EritrositSayisi = table.Column<double>(type: "float", nullable: true),
                    LökositSayisi = table.Column<double>(type: "float", nullable: true),
                    TrombositSayisi = table.Column<double>(type: "float", nullable: true),
                    ALT = table.Column<double>(type: "float", nullable: true),
                    AST = table.Column<double>(type: "float", nullable: true),
                    Ure = table.Column<double>(type: "float", nullable: true),
                    Kreatinin = table.Column<double>(type: "float", nullable: true),
                    Albumin = table.Column<double>(type: "float", nullable: true),
                    Glikoz = table.Column<double>(type: "float", nullable: true),
                    Sodyum = table.Column<double>(type: "float", nullable: true),
                    Potasyum = table.Column<double>(type: "float", nullable: true),
                    Klorür = table.Column<double>(type: "float", nullable: true),
                    OksijenDoygunlugu = table.Column<double>(type: "float", nullable: true),
                    KarbondioksitDoygunlugu = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanDegerleri", x => x.KanDegerleriId);
                    table.ForeignKey(
                        name: "FK_KanDegerleri_Muayeneler_MuayeneId",
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
                value: "9c6db854-79d3-4ec1-8d9d-9d190637d19c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SifreGecerlilikTarihi", "SifreOlusturmaTarihi" },
                values: new object[] { "70e8b689-d4c1-44ee-bd75-abecb0c67f76", "AQAAAAEAACcQAAAAEAG1nElE+tCCr/3UDc/KuigGr71VsVmcA5MMx3Ee3nI2YSM7G9Gef6XuSox16n5mJA==", "d5193fc1-202c-48b9-950f-3b77e77b70b4", new DateTime(3023, 10, 8, 14, 59, 10, 201, DateTimeKind.Local).AddTicks(5782), new DateTime(2024, 10, 8, 14, 59, 10, 199, DateTimeKind.Local).AddTicks(7350) });

            migrationBuilder.CreateIndex(
                name: "IX_KanDegerleri_MuayeneId",
                table: "KanDegerleri",
                column: "MuayeneId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KanDegerleri");

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
    }
}
