using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Migrations
{
    public partial class UpdateModelGrid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_RobotId",
                table: "Modules");

            migrationBuilder.AddColumn<Guid>(
                name: "GridId",
                table: "Robots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RobotId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Robots_GridId",
                table: "Robots",
                column: "GridId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_RobotId",
                table: "Modules",
                column: "RobotId",
                unique: true,
                filter: "[RobotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Grids_GridId",
                table: "Robots",
                column: "GridId",
                principalTable: "Grids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Grids_GridId",
                table: "Robots");

            migrationBuilder.DropIndex(
                name: "IX_Robots_GridId",
                table: "Robots");

            migrationBuilder.DropIndex(
                name: "IX_Modules_RobotId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "GridId",
                table: "Robots");

            migrationBuilder.AlterColumn<Guid>(
                name: "RobotId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_RobotId",
                table: "Modules",
                column: "RobotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
