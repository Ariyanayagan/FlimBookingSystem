using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flim.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Addconcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Seats",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "HeldTickets",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "HeldTickets");
        }
    }
}
