using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class ChangeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysFoods_DayFood_DayFoodId",
                table: "DaysFoods");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "DaysFoods");

            migrationBuilder.AlterColumn<Guid>(
                name: "DayFoodId",
                table: "DaysFoods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DaysFoods_DayFood_DayFoodId",
                table: "DaysFoods",
                column: "DayFoodId",
                principalTable: "DayFood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysFoods_DayFood_DayFoodId",
                table: "DaysFoods");

            migrationBuilder.AlterColumn<Guid>(
                name: "DayFoodId",
                table: "DaysFoods",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "DaysFoods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_DaysFoods_DayFood_DayFoodId",
                table: "DaysFoods",
                column: "DayFoodId",
                principalTable: "DayFood",
                principalColumn: "Id");
        }
    }
}
