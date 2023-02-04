using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class Measurement_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
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

            migrationBuilder.CreateTable(
                name: "MeasurementTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementTypes", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "MeasurementTypes");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_MeasurementTypeId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "MeasurementTypeId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Inventories");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Inventories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
