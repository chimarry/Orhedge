using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class AlternatingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyMaterialRatings_Students_AuthorId",
                table: "StudyMaterialRatings");

            migrationBuilder.DropIndex(
                name: "IX_StudyMaterialRatings_AuthorId",
                table: "StudyMaterialRatings");

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "StudyMaterials",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Students",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Courses",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Categories",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Students_Username",
                table: "Students",
                column: "Username");
            migrationBuilder.CreateIndex(
                 name: "IX_Students_Username",
                table: "Students",
                 column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Students_Username",
                table: "Students");

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "StudyMaterials",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Students",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Deleted",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterialRatings_AuthorId",
                table: "StudyMaterialRatings",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyMaterialRatings_Students_AuthorId",
                table: "StudyMaterialRatings",
                column: "AuthorId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
