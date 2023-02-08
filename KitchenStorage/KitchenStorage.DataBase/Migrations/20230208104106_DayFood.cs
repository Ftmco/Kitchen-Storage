using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class DayFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayFood",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meal = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayFood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaysFoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayFoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaysFoods_DayFood_DayFoodId",
                        column: x => x.DayFoodId,
                        principalTable: "DayFood",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DaysFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysFoods_DayFoodId",
                table: "DaysFoods",
                column: "DayFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DaysFoods_FoodId",
                table: "DaysFoods",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaysFoods");

            migrationBuilder.DropTable(
                name: "DayFood");
        }
    }
}
