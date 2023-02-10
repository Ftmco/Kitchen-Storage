using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class FoodHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Meal",
                table: "FoodHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "FoodHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoodHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodHistories_DayId",
                table: "FoodHistories",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodHistories_Day_DayId",
                table: "FoodHistories",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodHistories_Day_DayId",
                table: "FoodHistories");

            migrationBuilder.DropIndex(
                name: "IX_FoodHistories_DayId",
                table: "FoodHistories");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "FoodHistories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoodHistories");

            migrationBuilder.AlterColumn<byte>(
                name: "Meal",
                table: "FoodHistories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
