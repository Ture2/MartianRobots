using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Migrations
{
    public partial class GridEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RobotId",
                table: "Robots");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GridId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RobotId",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Grids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XAxisLength = table.Column<int>(type: "int", nullable: false),
                    YAxisLength = table.Column<int>(type: "int", nullable: false),
                    Planet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grids", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_GridId",
                table: "Modules",
                column: "GridId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_RobotId",
                table: "Modules",
                column: "RobotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Grids_GridId",
                table: "Modules",
                column: "GridId",
                principalTable: "Grids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules",
                column: "RobotId",
                principalTable: "Robots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Grids_GridId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Robots_RobotId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "Grids");

            migrationBuilder.DropIndex(
                name: "IX_Modules_GridId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_RobotId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "GridId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "RobotId",
                table: "Modules");

            migrationBuilder.AddColumn<int>(
                name: "RobotId",
                table: "Robots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
