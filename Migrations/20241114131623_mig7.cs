using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinerBilgiSistemi.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "KanDegeriValue",
                table: "KanTestiMuayene",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "KanDegeriValue",
                table: "KanTestiMuayene",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

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
    }
}
