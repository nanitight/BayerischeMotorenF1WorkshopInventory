using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_F1_WorkshopInventory.Migrations
{
    /// <inheritdoc />
    public partial class racedaytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "RaceDay",
                table: "GrandPrixResult",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RaceDay",
                table: "GrandPrixResult",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
