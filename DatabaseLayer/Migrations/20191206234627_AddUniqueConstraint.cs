using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_CourseId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "StudyMaterials",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterials_Uri",
                table: "StudyMaterials",
                column: "Uri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Index",
                table: "Students",
                column: "Index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name_Semester_StudyYear",
                table: "Courses",
                columns: new[] { "Name", "Semester", "StudyYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CourseId_Name",
                table: "Categories",
                columns: new[] { "CourseId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudyMaterials_Uri",
                table: "StudyMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Index",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Name_Semester_StudyYear",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CourseId_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "StudyMaterials",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CourseId",
                table: "Categories",
                column: "CourseId");
        }
    }
}
