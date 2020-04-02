using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class AddDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Students",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Registrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "Registrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Registrations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Privilege",
                table: "Registrations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Privilege",
                table: "Registrations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Students",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
