using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class editTableCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId1",
                table: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_UserId1",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Checkouts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Checkouts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_UserId",
                table: "Checkouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId",
                table: "Checkouts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId",
                table: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_UserId",
                table: "Checkouts");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Checkouts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_UserId1",
                table: "Checkouts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_AspNetUsers_UserId1",
                table: "Checkouts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
