using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.LibEntity.Migrations
{
    /// <inheritdoc />
    public partial class AddedownershiptoTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Task",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Task_OwnerId",
                table: "Task",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_OwnerId",
                table: "Task",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_OwnerId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_OwnerId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Task");
        }
    }
}
