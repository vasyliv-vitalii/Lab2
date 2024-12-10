using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedPasswordToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_UserId",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "BikeRoutes");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_UserId",
                table: "BikeRoutes",
                newName: "IX_BikeRoutes_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BikeRoutes",
                table: "BikeRoutes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BikeRoutes_Users_UserId",
                table: "BikeRoutes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BikeRoutes_Users_UserId",
                table: "BikeRoutes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BikeRoutes",
                table: "BikeRoutes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "BikeRoutes",
                newName: "Routes");

            migrationBuilder.RenameIndex(
                name: "IX_BikeRoutes_UserId",
                table: "Routes",
                newName: "IX_Routes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_UserId",
                table: "Routes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
