using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ilk3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Register",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "musteriTelNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "uyeAdi",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "uyeSoyadi",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_AppUserId",
                table: "Randevular",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_AppUserId",
                table: "Randevular",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_AppUserId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_AppUserId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "musteriTelNo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "uyeAdi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "uyeSoyadi",
                table: "AspNetUsers");
        }
    }
}
