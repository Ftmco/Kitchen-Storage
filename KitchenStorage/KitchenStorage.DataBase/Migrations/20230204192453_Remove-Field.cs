using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class RemoveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<double>(
                name: "AlertLimit",
                table: "Inventories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertLimit",
                table: "Inventories");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementTypeId",
                table: "Inventories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
