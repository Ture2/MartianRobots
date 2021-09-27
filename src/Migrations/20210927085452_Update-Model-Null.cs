using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Migrations
{
    public partial class UpdateModelNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.AlterColumn<Guid>(
                name: "GridId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.AlterColumn<Guid>(
                name: "GridId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id");
        }
    }
}
