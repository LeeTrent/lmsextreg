using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace lmsextreg.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    AgencyID = table.Column<string>(nullable: false),
                    AgencyName = table.Column<string>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    OPMCode = table.Column<string>(nullable: true),
                    TreasuryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.AgencyID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentStatus",
                columns: table => new
                {
                    StatusCode = table.Column<string>(nullable: false),
                    StatusLabel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentStatus", x => x.StatusCode);
                });

            migrationBuilder.CreateTable(
                name: "LMSProgram",
                columns: table => new
                {
                    LMSProgramID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LongName = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LMSProgram", x => x.LMSProgramID);
                });

            migrationBuilder.CreateTable(
                name: "SubAgency",
                columns: table => new
                {
                    SubAgencyID = table.Column<string>(nullable: false),
                    AgencyID = table.Column<string>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    OPMCode = table.Column<string>(nullable: true),
                    SubAgencyName = table.Column<string>(nullable: false),
                    TreasuryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAgency", x => x.SubAgencyID);
                    table.ForeignKey(
                        name: "FK_SubAgency_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusTransition",
                columns: table => new
                {
                    StatusTransitionID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FromStatusCode = table.Column<string>(nullable: false),
                    ToStatusCode = table.Column<string>(nullable: false),
                    TransitionCode = table.Column<string>(nullable: false),
                    TransitionLabel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTransition", x => x.StatusTransitionID);
                    table.ForeignKey(
                        name: "FK_StatusTransition_EnrollmentStatus_FromStatusCode",
                        column: x => x.FromStatusCode,
                        principalTable: "EnrollmentStatus",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusTransition_EnrollmentStatus_ToStatusCode",
                        column: x => x.ToStatusCode,
                        principalTable: "EnrollmentStatus",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AgencyID = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DateExpired = table.Column<DateTime>(nullable: false),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    SubAgencyID = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_SubAgency_SubAgencyID",
                        column: x => x.SubAgencyID,
                        principalTable: "SubAgency",
                        principalColumn: "SubAgencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramApprover",
                columns: table => new
                {
                    LMSProgramID = table.Column<int>(nullable: false),
                    ApproverUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramApprover", x => new { x.LMSProgramID, x.ApproverUserId });
                    table.ForeignKey(
                        name: "FK_ProgramApprover_AspNetUsers_ApproverUserId",
                        column: x => x.ApproverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramApprover_LMSProgram_LMSProgramID",
                        column: x => x.LMSProgramID,
                        principalTable: "LMSProgram",
                        principalColumn: "LMSProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramEnrollment",
                columns: table => new
                {
                    ProgramEnrollmentID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApproverUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    LMSProgramID = table.Column<int>(nullable: false),
                    StatusCode = table.Column<string>(nullable: false),
                    StudentUserId = table.Column<string>(nullable: false),
                    UserCreated = table.Column<string>(nullable: false),
                    UserLastUpdated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEnrollment", x => x.ProgramEnrollmentID);
                    table.ForeignKey(
                        name: "FK_ProgramEnrollment_AspNetUsers_ApproverUserId",
                        column: x => x.ApproverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramEnrollment_LMSProgram_LMSProgramID",
                        column: x => x.LMSProgramID,
                        principalTable: "LMSProgram",
                        principalColumn: "LMSProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEnrollment_EnrollmentStatus_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "EnrollmentStatus",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramEnrollment_AspNetUsers_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentHistory",
                columns: table => new
                {
                    EnrollmentHistoryID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActorRemarks = table.Column<string>(nullable: true),
                    ActorUserId = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ProgramEnrollmentID = table.Column<int>(nullable: false),
                    StatusTransitionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentHistory", x => x.EnrollmentHistoryID);
                    table.ForeignKey(
                        name: "FK_EnrollmentHistory_AspNetUsers_ActorUserId",
                        column: x => x.ActorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentHistory_ProgramEnrollment_ProgramEnrollmentID",
                        column: x => x.ProgramEnrollmentID,
                        principalTable: "ProgramEnrollment",
                        principalColumn: "ProgramEnrollmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentHistory_StatusTransition_StatusTransitionID",
                        column: x => x.StatusTransitionID,
                        principalTable: "StatusTransition",
                        principalColumn: "StatusTransitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AgencyID",
                table: "AspNetUsers",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubAgencyID",
                table: "AspNetUsers",
                column: "SubAgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentHistory_ActorUserId",
                table: "EnrollmentHistory",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentHistory_ProgramEnrollmentID",
                table: "EnrollmentHistory",
                column: "ProgramEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentHistory_StatusTransitionID",
                table: "EnrollmentHistory",
                column: "StatusTransitionID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentStatus_StatusLabel",
                table: "EnrollmentStatus",
                column: "StatusLabel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramApprover_ApproverUserId",
                table: "ProgramApprover",
                column: "ApproverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollment_ApproverUserId",
                table: "ProgramEnrollment",
                column: "ApproverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollment_StatusCode",
                table: "ProgramEnrollment",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollment_StudentUserId",
                table: "ProgramEnrollment",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEnrollment_LMSProgramID_StudentUserId",
                table: "ProgramEnrollment",
                columns: new[] { "LMSProgramID", "StudentUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusTransition_ToStatusCode",
                table: "StatusTransition",
                column: "ToStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTransition_FromStatusCode_ToStatusCode",
                table: "StatusTransition",
                columns: new[] { "FromStatusCode", "ToStatusCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubAgency_AgencyID",
                table: "SubAgency",
                column: "AgencyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EnrollmentHistory");

            migrationBuilder.DropTable(
                name: "ProgramApprover");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProgramEnrollment");

            migrationBuilder.DropTable(
                name: "StatusTransition");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LMSProgram");

            migrationBuilder.DropTable(
                name: "EnrollmentStatus");

            migrationBuilder.DropTable(
                name: "SubAgency");

            migrationBuilder.DropTable(
                name: "Agency");
        }
    }
}
