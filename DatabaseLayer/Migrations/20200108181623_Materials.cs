using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class Materials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "StudyPrograms",
                columns: table => new
                {
                    StudyProgramId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPrograms", x => x.StudyProgramId);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudyPrograms",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    StudyProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudyPrograms", x => new { x.StudyProgramId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CourseStudyPrograms_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CourseStudyPrograms_StudyPrograms_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "StudyPrograms",
                        principalColumn: "StudyProgramId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudyPrograms_CourseId",
                table: "CourseStudyPrograms",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudyPrograms");

            migrationBuilder.DropTable(
                name: "StudyPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
