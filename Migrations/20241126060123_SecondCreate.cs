using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList1.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_tb_Users_tb_UserId",
                table: "Tasks_tb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_tb",
                table: "Users_tb");

            migrationBuilder.RenameTable(
                name: "Users_tb",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_tb_Users_UserId",
                table: "Tasks_tb",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_tb_Users_UserId",
                table: "Tasks_tb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users_tb");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users_tb",
                newName: "Password");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_tb",
                table: "Users_tb",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_tb_Users_tb_UserId",
                table: "Tasks_tb",
                column: "UserId",
                principalTable: "Users_tb",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
