using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class sondegilsinnnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_kullaniciId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Randevular",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "kullaniciId",
                table: "Randevular",
                newName: "kullanici_Id");

            migrationBuilder.AddColumn<int>(
                name: "randevuId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Randevular",
                table: "Randevular",
                column: "randevuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Randevular",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "randevuId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "kullanici_Id",
                table: "Randevular",
                newName: "kullaniciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Randevular",
                table: "Randevular",
                column: "kullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_kullaniciId",
                table: "Randevular",
                column: "kullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
