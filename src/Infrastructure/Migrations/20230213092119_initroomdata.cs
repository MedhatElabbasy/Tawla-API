using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tawala.Infrastructure.Migrations
{
    public partial class initroomdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResposableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResposablePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_OptionSetItem_BuildingTypeId",
                        column: x => x.BuildingTypeId,
                        principalTable: "OptionSetItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FloorNumberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FloorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floors_OptionSetItem_FloorNumberId",
                        column: x => x.FloorNumberId,
                        principalTable: "OptionSetItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floors_OptionSetItem_FloorTypeId",
                        column: x => x.FloorTypeId,
                        principalTable: "OptionSetItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FloorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BackGroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatBackGroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableBackGroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    hight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_AppAttachment_DefaultImageId",
                        column: x => x.DefaultImageId,
                        principalTable: "AppAttachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImages_AppAttachment_ImageId",
                        column: x => x.ImageId,
                        principalTable: "AppAttachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionLeft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionRight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionTop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PositionBottom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DefaultImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    TableTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsFriday = table.Column<bool>(type: "bit", nullable: false),
                    IsMonday = table.Column<bool>(type: "bit", nullable: false),
                    IsSaturday = table.Column<bool>(type: "bit", nullable: false),
                    IsSunday = table.Column<bool>(type: "bit", nullable: false),
                    IsThursday = table.Column<bool>(type: "bit", nullable: false),
                    IsTuesday = table.Column<bool>(type: "bit", nullable: false),
                    IsWednesday = table.Column<bool>(type: "bit", nullable: false),
                    StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    JsonModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSet = table.Column<int>(type: "int", nullable: false),
                    MaxNumberOfSet = table.Column<int>(type: "int", nullable: false),
                    DefaultNumberOfSet = table.Column<int>(type: "int", nullable: false),
                    VipPricePerSet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VipPricePerTable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StandardPricePerSet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StandardPricePerTable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTables_AppAttachment_DefaultImageId",
                        column: x => x.DefaultImageId,
                        principalTable: "AppAttachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTables_OptionSetItem_TableTypeId",
                        column: x => x.TableTypeId,
                        principalTable: "OptionSetItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTables_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomTableImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTableImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTableImages_AppAttachment_ImageId",
                        column: x => x.ImageId,
                        principalTable: "AppAttachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomTableImages_RoomTables_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BranchId",
                table: "Buildings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BuildingTypeId",
                table: "Buildings",
                column: "BuildingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_RestaurantId",
                table: "Buildings",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BuildingId",
                table: "Floors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorNumberId",
                table: "Floors",
                column: "FloorNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorTypeId",
                table: "Floors",
                column: "FloorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_ImageId",
                table: "RoomImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DefaultImageId",
                table: "Rooms",
                column: "DefaultImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTableImages_ImageId",
                table: "RoomTableImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTableImages_RoomId",
                table: "RoomTableImages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTables_DefaultImageId",
                table: "RoomTables",
                column: "DefaultImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTables_RoomId",
                table: "RoomTables",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTables_TableTypeId",
                table: "RoomTables",
                column: "TableTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.DropTable(
                name: "RoomTableImages");

            migrationBuilder.DropTable(
                name: "RoomTables");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
