using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class ChangeAttributeCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudyProgram",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyProgram",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "StudyYear",
                table: "Courses",
                nullable: false,
                defaultValue: "");
        }
    }
}
