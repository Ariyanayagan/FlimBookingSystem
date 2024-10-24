using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flim.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class columnremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seats",
                type: "integer",
                nullable: true);
        }
    }
}
