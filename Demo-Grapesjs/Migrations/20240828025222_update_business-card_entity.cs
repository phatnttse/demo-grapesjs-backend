using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_Grapesjs.Migrations
{
    /// <inheritdoc />
    public partial class update_businesscard_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "BusinessCards");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "BusinessCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "BusinessCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "BusinessCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
