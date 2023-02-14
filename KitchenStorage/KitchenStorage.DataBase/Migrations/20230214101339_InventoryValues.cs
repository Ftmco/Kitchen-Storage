using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class InventoryValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventoryValue",
                table: "InventoryHistoryList",
                newName: "NextValue");

            migrationBuilder.AddColumn<double>(
                name: "CurrentValue",
                table: "InventoryHistoryList",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentValue",
                table: "InventoryHistoryList");

            migrationBuilder.RenameColumn(
                name: "NextValue",
                table: "InventoryHistoryList",
                newName: "InventoryValue");
        }
    }
}
