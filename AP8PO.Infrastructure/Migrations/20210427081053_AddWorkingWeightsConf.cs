using Microsoft.EntityFrameworkCore.Migrations;

namespace AP8POSecretary.Infrastructure.Migrations
{
    public partial class AddWorkingWeightsConf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPointsWeights",
                table: "WorkingPointsWeights");

            migrationBuilder.RenameTable(
                name: "WorkingPointsWeights",
                newName: "WorkingPointsWeight",
                newSchema: "Secretary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPointsWeight",
                schema: "Secretary",
                table: "WorkingPointsWeight",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingPointsWeight",
                schema: "Secretary",
                table: "WorkingPointsWeight");

            migrationBuilder.RenameTable(
                name: "WorkingPointsWeight",
                schema: "Secretary",
                newName: "WorkingPointsWeights");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingPointsWeights",
                table: "WorkingPointsWeights",
                column: "Id");
        }
    }
}
