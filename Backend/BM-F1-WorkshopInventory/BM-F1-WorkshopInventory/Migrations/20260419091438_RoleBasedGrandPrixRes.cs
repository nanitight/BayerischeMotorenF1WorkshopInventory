using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_F1_WorkshopInventory.Migrations
{
    /// <inheritdoc />
    public partial class RoleBasedGrandPrixRes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GrandPrixResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RaceDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    PointsScored = table.Column<double>(type: "double precision", nullable: false),
                    PositionInTeamGrid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrandPrixResult", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrandPrixResult");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
