using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_Grapesjs.Migrations
{
    /// <inheritdoc />
    public partial class update_entity_for_NameCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNameCards_Users_Guid",
                table: "UserNameCards");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "UserNameCards",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserNameCards_Guid",
                table: "UserNameCards",
                newName: "IX_UserNameCards_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNameCards_Users_UserId",
                table: "UserNameCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNameCards_Users_UserId",
                table: "UserNameCards");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserNameCards",
                newName: "Guid");

            migrationBuilder.RenameIndex(
                name: "IX_UserNameCards_UserId",
                table: "UserNameCards",
                newName: "IX_UserNameCards_Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNameCards_Users_Guid",
                table: "UserNameCards",
                column: "Guid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
