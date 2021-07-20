using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "courses",
                newName: "code");

            migrationBuilder.AddColumn<string>(
                name: "career",
                table: "courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "credits",
                table: "courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "career",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "credits",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "courses",
                newName: "description");
        }
    }
}
