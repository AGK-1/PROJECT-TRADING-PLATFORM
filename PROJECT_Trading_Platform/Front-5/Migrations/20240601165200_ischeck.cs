using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Front5.Migrations
{
    /// <inheritdoc />
    public partial class ischeck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ischeck",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ischeck",
                table: "Cards");
        }
    }
}
