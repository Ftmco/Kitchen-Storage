using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class UpdateInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementTypeId",
                table: "Inventories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Inventories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_MeasurementTypeId",
                table: "Inventories",
                column: "MeasurementTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_MeasurementTypes_MeasurementTypeId",
                table: "Inventories",
                column: "MeasurementTypeId",
                principalTable: "MeasurementTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_MeasurementTypes_MeasurementTypeId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_MeasurementTypeId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "MeasurementTypeId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Inventories");
        }
    }
}
