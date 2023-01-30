using Microsoft.EntityFrameworkCore.Migrations;

namespace Tawala.Infrastructure.Migrations
{
    public partial class branshphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Branchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber2",
                table: "Branchs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Branchs");

            migrationBuilder.DropColumn(
                name: "PhoneNumber2",
                table: "Branchs");
        }
    }
}
