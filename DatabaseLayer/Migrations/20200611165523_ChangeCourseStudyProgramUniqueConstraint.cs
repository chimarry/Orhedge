using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class ChangeCourseStudyProgramUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms",
                columns: new[] { "StudyProgram", "CourseId", "Semester" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms",
                columns: new[] { "StudyProgram", "CourseId" });
        }
    }
}
