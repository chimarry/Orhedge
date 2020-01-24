using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class MoveSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "CourseStudyPrograms",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "CourseStudyPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Courses",
                nullable: false,
                defaultValue: "");
        }
    }
}
