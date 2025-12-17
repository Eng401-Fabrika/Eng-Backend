using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eng_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProblemAndQuizEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problems_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<string>(type: "text", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuizPoints = table.Column<int>(type: "integer", nullable: false),
                    TaskPoints = table.Column<int>(type: "integer", nullable: false),
                    TotalPoints = table.Column<int>(type: "integer", nullable: false),
                    QuizzesCompleted = table.Column<int>(type: "integer", nullable: false),
                    TasksCompleted = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserScores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProblemAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProblemId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsNotified = table.Column<bool>(type: "boolean", nullable: false),
                    NotifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemAssignments_AspNetUsers_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProblemAssignments_AspNetUsers_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProblemAssignments_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedAnswerIndex = table.Column<int>(type: "integer", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    PointsEarned = table.Column<int>(type: "integer", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewNotes = table.Column<string>(type: "text", nullable: true),
                    PointsAwarded = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solutions_AspNetUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solutions_ProblemAssignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "ProblemAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolutionFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    FileKey = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolutionFiles_Solutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "efa73bb2-f247-40f9-a6d1-5a5f1d627076", new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "9ee2c501-0013-46db-a0c4-7884e16ee92f", new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "40c0cd63-ac7f-406d-9647-ae2e244347d6", new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 14, 30, 17, 658, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.CreateIndex(
                name: "IX_ProblemAssignments_AssignedByUserId",
                table: "ProblemAssignments",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemAssignments_AssignedToUserId",
                table: "ProblemAssignments",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemAssignments_ProblemId",
                table: "ProblemAssignments",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_CreatedByUserId",
                table: "Problems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DocumentId",
                table: "Questions",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SolutionFiles_SolutionId",
                table: "SolutionFiles",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_AssignmentId",
                table: "Solutions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_ReviewedByUserId",
                table: "Solutions",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserId_QuestionId",
                table: "UserAnswers",
                columns: new[] { "UserId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_UserId",
                table: "UserScores",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolutionFiles");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "UserScores");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ProblemAssignments");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "51a0a8cc-6302-4d5c-97f1-bda3c174f41d", new DateTime(2025, 12, 17, 10, 5, 2, 802, DateTimeKind.Utc).AddTicks(9970), new DateTime(2025, 12, 17, 10, 5, 2, 802, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "ed405ea7-df13-4b51-b768-a4b5c4d48d6c", new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(10), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "9e261631-b5da-4d2d-9878-4a7b4b49040a", new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(10), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(140), new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 12, 17, 10, 5, 2, 803, DateTimeKind.Utc).AddTicks(210));
        }
    }
}
