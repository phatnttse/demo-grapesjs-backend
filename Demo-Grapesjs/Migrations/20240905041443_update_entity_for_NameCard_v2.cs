using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_Grapesjs.Migrations
{
    /// <inheritdoc />
    public partial class update_entity_for_NameCard_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "NameCardTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "NameCardTemplates");
        }
    }
}
