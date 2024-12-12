using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModeleAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTeacher",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsTeacher",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                table: "Teachers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "Students",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
