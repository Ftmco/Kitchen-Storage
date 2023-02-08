using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class Add_FoodId_Prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Norms_Foods_FoodId",
                table: "Norms");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Norms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Norms_Foods_FoodId",
                table: "Norms",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Norms_Foods_FoodId",
                table: "Norms");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Norms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Norms_Foods_FoodId",
                table: "Norms",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");
        }
    }
}
