using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class ChangeMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Students",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Students",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 63);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 63);
        }
    }
}
