using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList1.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "UserModelUserId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserModelUserId",
                table: "Tasks",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserModelUserId",
                table: "Tasks",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserModelUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserModelUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
