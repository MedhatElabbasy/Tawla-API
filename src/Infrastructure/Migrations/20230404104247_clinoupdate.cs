using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tawala.Infrastructure.Migrations
{
    public partial class clinoupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "ClientNotifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientNotifications_RestaurantId",
                table: "ClientNotifications",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientNotifications_Restaurants_RestaurantId",
                table: "ClientNotifications",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientNotifications_Restaurants_RestaurantId",
                table: "ClientNotifications");

            migrationBuilder.DropIndex(
                name: "IX_ClientNotifications_RestaurantId",
                table: "ClientNotifications");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "ClientNotifications");
        }
    }
}
