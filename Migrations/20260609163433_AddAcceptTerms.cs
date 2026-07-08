using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddAcceptTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptTerms",
                table: "registerModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTerms",
                table: "registerModels");
        }
    }
}
