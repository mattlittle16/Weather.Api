using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KeyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_LogType_Id",
                table: "Log");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogTypeId",
                table: "Log",
                column: "LogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_LogType_LogTypeId",
                table: "Log",
                column: "LogTypeId",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_LogType_LogTypeId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_LogTypeId",
                table: "Log");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_LogType_Id",
                table: "Log",
                column: "Id",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
