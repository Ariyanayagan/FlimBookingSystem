using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flim.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class addextracolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seats",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seats");
        }
    }
}
