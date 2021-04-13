using Microsoft.EntityFrameworkCore.Migrations;

namespace AP8POSecretary.Infrastructure.Migrations
{
    public partial class AddWorkingPointsRelatedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WorkingPoints",
                schema: "Secretary",
                table: "Employee",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WorkingPointsWithEng",
                schema: "Secretary",
                table: "Employee",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingPoints",
                schema: "Secretary",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "WorkingPointsWithEng",
                schema: "Secretary",
                table: "Employee");
        }
    }
}
