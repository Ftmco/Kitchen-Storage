using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class InventoryHistoryList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextValue",
                table: "InventoryHistories");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "InventoryHistories");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "InventoryHistories",
                newName: "GenerateId");

            migrationBuilder.CreateTable(
                name: "InventoryHistoryList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    InventoryValue = table.Column<double>(type: "float", nullable: false),
                    InventoryHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHistoryList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryHistoryList_InventoryHistories_InventoryHistoryId",
                        column: x => x.InventoryHistoryId,
                        principalTable: "InventoryHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistoryList_InventoryHistoryId",
                table: "InventoryHistoryList",
                column: "InventoryHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryHistoryList");

            migrationBuilder.RenameColumn(
                name: "GenerateId",
                table: "InventoryHistories",
                newName: "InventoryId");

            migrationBuilder.AddColumn<double>(
                name: "NextValue",
                table: "InventoryHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "InventoryHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
