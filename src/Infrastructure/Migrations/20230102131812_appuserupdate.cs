using Microsoft.EntityFrameworkCore.Migrations;

namespace Tawala.Infrastructure.Migrations
{
    public partial class appuserupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumer",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumer",
                table: "AspNetUsers");
        }
    }
}
