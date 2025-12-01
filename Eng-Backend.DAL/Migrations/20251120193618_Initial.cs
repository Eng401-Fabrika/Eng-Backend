using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eng_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "a86daeee-d200-4e85-8237-4b045c81fb7c", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3890), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "251eda3d-694b-4b6e-99af-1ebfbea3609d", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3920), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "UpdatedAt" },
                values: new object[] { "fe77c512-00a8-4c98-9a72-b6dd59b26a0f", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3930), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "GrantedAt",
                value: new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090));
        }
    }
}
