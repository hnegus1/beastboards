using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeastBoards.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedLevelCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BeastBoards_LeaderboardTimings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BeastBoards_LeaderboardTimings");
        }
    }
}
