using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class TypeConvert : Migration
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

            migrationBuilder.AddColumn<byte>(
                name: "Meal",
                table: "FoodHistories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "TypeConvert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromValue = table.Column<double>(type: "float", nullable: false),
                    ToValue = table.Column<double>(type: "float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeConvert", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Id_GroupId_TypeId",
                table: "Inventories",
                columns: new[] { "Id", "GroupId", "TypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_TypeConvert_FromTypeId_ToTypeId",
                table: "TypeConvert",
                columns: new[] { "FromTypeId", "ToTypeId" });

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

            migrationBuilder.DropTable(
                name: "TypeConvert");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_Id_GroupId_TypeId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Meal",
                table: "FoodHistories");

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
