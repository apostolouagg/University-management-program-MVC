using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ergasia2mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CourseSemester = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ProfessorAFM = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Users_ProfessorAFM",
                        column: x => x.ProfessorAFM,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    AFM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ProfessorUsername = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.AFM);
                    table.ForeignKey(
                        name: "FK_Professors_Users_ProfessorUsername",
                        column: x => x.ProfessorUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    SecretaryUsername = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_Secretaries_Users_SecretaryUsername",
                        column: x => x.SecretaryUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    StudentUsername = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RegistrationNumber);
                    table.ForeignKey(
                        name: "FK_Students_Users_StudentUsername",
                        column: x => x.StudentUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseHasStudents",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeCourseStudent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseHasStudents", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_CourseHasStudents_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseHasStudents_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "RegistrationNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseHasStudents_StudentID",
                table: "CourseHasStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorAFM",
                table: "Courses",
                column: "ProfessorAFM");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_ProfessorUsername",
                table: "Professors",
                column: "ProfessorUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaries_SecretaryUsername",
                table: "Secretaries",
                column: "SecretaryUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentUsername",
                table: "Students",
                column: "StudentUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseHasStudents");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Secretaries");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
