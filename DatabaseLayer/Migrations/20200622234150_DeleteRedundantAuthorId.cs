using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class DeleteRedundantAuthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "StudyMaterialRatings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "StudyMaterialRatings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
