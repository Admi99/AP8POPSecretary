using Microsoft.EntityFrameworkCore.Migrations;

namespace AP8POSecretary.Infrastructure.Migrations
{
    public partial class InitSqlLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Secretary");

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    WholeName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalEmail = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    isDoctorant = table.Column<bool>(type: "INTEGER", nullable: false),
                    CommitmentRate = table.Column<double>(type: "REAL", nullable: false),
                    WorkingPoints = table.Column<double>(type: "REAL", nullable: false),
                    WorkingPointsWithEng = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Shortcut = table.Column<string>(type: "TEXT", nullable: true),
                    StudyYear = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    StudentsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyType = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Shortcut = table.Column<string>(type: "TEXT", nullable: true),
                    LectureCount = table.Column<int>(type: "INTEGER", nullable: false),
                    SeminareCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PractiseCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Credit = table.Column<int>(type: "INTEGER", nullable: false),
                    WeeksCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassSize = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletionType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingPointsWeight",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkingWeightTypes = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPointsWeight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupSubject",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSubject_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Secretary",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "Secretary",
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingLabel",
                schema: "Secretary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    StudentsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    EmploymentPoints = table.Column<double>(type: "REAL", nullable: false),
                    EventType = table.Column<int>(type: "INTEGER", nullable: false),
                    HoursCount = table.Column<int>(type: "INTEGER", nullable: false),
                    WeekCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingLabel_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Secretary",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingLabel_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "Secretary",
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubject_GroupId",
                schema: "Secretary",
                table: "GroupSubject",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubject_SubjectId",
                schema: "Secretary",
                table: "GroupSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingLabel_EmployeeId",
                schema: "Secretary",
                table: "WorkingLabel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingLabel_SubjectId",
                schema: "Secretary",
                table: "WorkingLabel",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSubject",
                schema: "Secretary");

            migrationBuilder.DropTable(
                name: "WorkingLabel",
                schema: "Secretary");

            migrationBuilder.DropTable(
                name: "WorkingPointsWeight",
                schema: "Secretary");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Secretary");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Secretary");

            migrationBuilder.DropTable(
                name: "Subject",
                schema: "Secretary");
        }
    }
}
