using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class addColm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId",
                table: "Checkouts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Checkouts",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_UserId",
                table: "Checkouts",
                newName: "IX_Checkouts_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId1",
                table: "Checkouts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId1",
                table: "Checkouts");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Checkouts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_UserId1",
                table: "Checkouts",
                newName: "IX_Checkouts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId",
                table: "Checkouts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
