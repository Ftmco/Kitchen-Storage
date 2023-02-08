using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenStorage.DataBase.Migrations
{
    public partial class Days : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "DayFood");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "DayFood");

            migrationBuilder.AddColumn<Guid>(
                name: "DayId",
                table: "DayFood",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayFood_DayId",
                table: "DayFood",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayFood_Day_DayId",
                table: "DayFood",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayFood_Day_DayId",
                table: "DayFood");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropIndex(
                name: "IX_DayFood_DayId",
                table: "DayFood");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "DayFood");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "DayFood",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "DayNumber",
                table: "DayFood",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
