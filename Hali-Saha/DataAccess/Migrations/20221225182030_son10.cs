using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class son10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    TesisId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    RandevuDegerlendirmesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandevuId = table.Column<int>(type: "int", nullable: false),
                    RandevuPuani = table.Column<int>(type: "int", nullable: false),
                    SelectedTesis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    randevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => new { x.TesisId, x.KullaniciId });
                    table.ForeignKey(
                        name: "FK_Randevular_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Tesisler_TesisId",
                        column: x => x.TesisId,
                        principalTable: "Tesisler",
                        principalColumn: "TesisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");
        }
    }
}
