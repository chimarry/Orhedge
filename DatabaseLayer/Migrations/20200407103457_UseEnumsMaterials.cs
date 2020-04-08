using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class UseEnumsMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudyPrograms_StudyPrograms_StudyProgramId",
                table: "CourseStudyPrograms");

            migrationBuilder.DropTable(
                name: "StudyPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms");

            migrationBuilder.DropColumn(
                name: "StudyProgramId",
                table: "CourseStudyPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "CourseStudyPrograms",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StudyProgram",
                table: "CourseStudyPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms",
                columns: new[] { "StudyProgram", "CourseId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms");

            migrationBuilder.DropColumn(
                name: "StudyProgram",
                table: "CourseStudyPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "CourseStudyPrograms",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramId",
                table: "CourseStudyPrograms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudyPrograms",
                table: "CourseStudyPrograms",
                columns: new[] { "StudyProgramId", "CourseId" });

            migrationBuilder.CreateTable(
                name: "StudyPrograms",
                columns: table => new
                {
                    StudyProgramId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPrograms", x => x.StudyProgramId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudyPrograms_StudyPrograms_StudyProgramId",
                table: "CourseStudyPrograms",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "StudyProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
