using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class ChangeRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Group_GroupId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_GroupId",
                table: "Inventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventories_GroupId",
                table: "Inventories",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Group_GroupId",
                table: "Inventories",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
