using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList1.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_tb_Users_UserId",
                table: "Tasks_tb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks_tb",
                table: "Tasks_tb");

            migrationBuilder.RenameTable(
                name: "Tasks_tb",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_tb_UserId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Tasks_tb");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks_tb",
                newName: "IX_Tasks_tb_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks_tb",
                table: "Tasks_tb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_tb_Users_UserId",
                table: "Tasks_tb",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
