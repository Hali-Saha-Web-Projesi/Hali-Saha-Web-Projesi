using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class sondegilsinnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_Kullanici_Id",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "Kullanici_Id",
                table: "Randevular",
                newName: "kullaniciId");

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
                newName: "Kullanici_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_Kullanici_Id",
                table: "Randevular",
                column: "Kullanici_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
