using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class sonoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_KullaniciId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Randevular",
                newName: "kullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                newName: "IX_Randevular_kullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_kullaniciId",
                table: "Randevular",
                column: "kullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_kullaniciId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "kullaniciId",
                table: "Randevular",
                newName: "KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_kullaniciId",
                table: "Randevular",
                newName: "IX_Randevular_KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_KullaniciId",
                table: "Randevular",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
