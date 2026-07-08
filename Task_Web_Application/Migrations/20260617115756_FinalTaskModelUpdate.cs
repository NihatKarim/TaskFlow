using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class FinalTaskModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addTask");

            migrationBuilder.DropColumn(
                name: "CompletedBy",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "InProgressBy",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "InProgressByEmail",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "End",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "From",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "Sub",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "TaskHistories",
                newName: "ChangedBy");

            migrationBuilder.RenameColumn(
                name: "Task",
                table: "TaskHistories",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "ArchivedOn",
                table: "TaskHistories",
                newName: "ChangedAt");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TaskHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "ChangedBy",
                table: "TaskHistories",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "ChangedAt",
                table: "TaskHistories",
                newName: "ArchivedOn");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "TaskHistories",
                newName: "Task");

            migrationBuilder.AddColumn<string>(
                name: "CompletedBy",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InProgressBy",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InProgressByEmail",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "TaskHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "TaskHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "TaskHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sub",
                table: "TaskHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "addTask",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Sub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addTask", x => x.ID);
                });
        }
    }
}
