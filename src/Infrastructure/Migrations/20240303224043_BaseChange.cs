using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_LogType_LogTypeId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_LogTypeId",
                table: "Log");

            migrationBuilder.RenameColumn(
                name: "LogTypeId",
                table: "LogType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "Log",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_LogType_Id",
                table: "Log",
                column: "Id",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_LogType_Id",
                table: "Log");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LogType",
                newName: "LogTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Log",
                newName: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogTypeId",
                table: "Log",
                column: "LogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_LogType_LogTypeId",
                table: "Log",
                column: "LogTypeId",
                principalTable: "LogType",
                principalColumn: "LogTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
