using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eng_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    FileKey = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRoles",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRoles", x => new { x.DocumentId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_DocumentRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRoles_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRoles_RoleId",
                table: "DocumentRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatedByUserId",
                table: "Documents",
                column: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentRoles");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "4498b82e-e974-4717-a5f0-2d3858da5122", new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "a4bd60ad-c3bc-4ddc-a317-8b15bf89eb44", new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "5f162fd6-9663-47b8-b0e1-5a33e8cf8276", new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8470), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8470), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480), new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8480) });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 20, 19, 36, 18, 588, DateTimeKind.Utc).AddTicks(8550));
        }
    }
}
