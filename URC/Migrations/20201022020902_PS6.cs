using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class PS6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    OpportunityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Professor = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: false),
                    RoleDescription = table.Column<string>(maxLength: 5000, nullable: false),
                    Responsibilities = table.Column<string>(maxLength: 3000, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Mentor = table.Column<string>(maxLength: 50, nullable: true),
                    PostedDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Pay = table.Column<double>(nullable: false),
                    IsFilled = table.Column<bool>(nullable: false),
                    ProfessorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.OpportunityId);
                });

            migrationBuilder.CreateTable(
                name: "SearchTags",
                columns: table => new
                {
                    SearchTagId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchTags", x => x.SearchTagId);
                });

            migrationBuilder.CreateTable(
                name: "StudentApplications",
                columns: table => new
                {
                    StudentApplicationId = table.Column<int>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    ResumeFilename = table.Column<string>(nullable: true),
                    ExpectedGraduation = table.Column<DateTime>(nullable: false),
                    DegreePlan = table.Column<string>(maxLength: 10, nullable: true),
                    GPA = table.Column<double>(nullable: false),
                    UId = table.Column<string>(nullable: true),
                    Availability = table.Column<int>(nullable: false),
                    PersonalStatement = table.Column<string>(maxLength: 1000, nullable: true),
                    IsLookingForPosition = table.Column<bool>(nullable: false),
                    ApplicationDate = table.Column<DateTime>(nullable: false),
                    TimeModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentApplications", x => x.StudentApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities_SearchTags",
                columns: table => new
                {
                    OpportunityId = table.Column<int>(nullable: false),
                    SearchTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities_SearchTags", x => new { x.OpportunityId, x.SearchTagId });
                    table.ForeignKey(
                        name: "FK_Opportunities_SearchTags_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "OpportunityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_SearchTags_SearchTags_SearchTagId",
                        column: x => x.SearchTagId,
                        principalTable: "SearchTags",
                        principalColumn: "SearchTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    ApplicationStudentApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_StudentApplications_ApplicationStudentApplicationId",
                        column: x => x.ApplicationStudentApplicationId,
                        principalTable: "StudentApplications",
                        principalColumn: "StudentApplicationId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ApplicationStudentApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_Interests_StudentApplications_ApplicationStudentApplicationId",
                        column: x => x.ApplicationStudentApplicationId,
                        principalTable: "StudentApplications",
                        principalColumn: "StudentApplicationId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ApplicationStudentApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skills_StudentApplications_ApplicationStudentApplicationId",
                        column: x => x.ApplicationStudentApplicationId,
                        principalTable: "StudentApplications",
                        principalColumn: "StudentApplicationId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities_PreferredSkills",
                columns: table => new
                {
                    OpportunityId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities_PreferredSkills", x => new { x.OpportunityId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_Opportunities_PreferredSkills_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "OpportunityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_PreferredSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities_RequiredSkills",
                columns: table => new
                {
                    OpportunityId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities_RequiredSkills", x => new { x.OpportunityId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_Opportunities_RequiredSkills_Opportunities_OpportunityId",
                        column: x => x.OpportunityId,
                        principalTable: "Opportunities",
                        principalColumn: "OpportunityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_RequiredSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ApplicationStudentApplicationId",
                table: "Courses",
                column: "ApplicationStudentApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_ApplicationStudentApplicationId",
                table: "Interests",
                column: "ApplicationStudentApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_PreferredSkills_SkillId",
                table: "Opportunities_PreferredSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_RequiredSkills_SkillId",
                table: "Opportunities_RequiredSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_SearchTags_SearchTagId",
                table: "Opportunities_SearchTags",
                column: "SearchTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApplicationStudentApplicationId",
                table: "Skills",
                column: "ApplicationStudentApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Opportunities_PreferredSkills");

            migrationBuilder.DropTable(
                name: "Opportunities_RequiredSkills");

            migrationBuilder.DropTable(
                name: "Opportunities_SearchTags");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "SearchTags");

            migrationBuilder.DropTable(
                name: "StudentApplications");
        }
    }
}
