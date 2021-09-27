using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Migrations
{
    public partial class UpdateModelCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
