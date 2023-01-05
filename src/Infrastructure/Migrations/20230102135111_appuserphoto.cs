using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tawala.Infrastructure.Migrations
{
    public partial class appuserphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId1",
                table: "AspNetUsers",
                column: "PhotoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppAttachment_PhotoId1",
                table: "AspNetUsers",
                column: "PhotoId1",
                principalTable: "AppAttachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppAttachment_PhotoId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId1",
                table: "AspNetUsers");
        }
    }
}
