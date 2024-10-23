using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flim.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class addamount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Films",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Films");
        }
    }
}
