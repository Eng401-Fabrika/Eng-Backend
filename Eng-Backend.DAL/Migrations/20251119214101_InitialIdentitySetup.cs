using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eng_Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentitySetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsSystemRole = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "Description", "IsSystemRole", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "a86daeee-d200-4e85-8237-4b045c81fb7c", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3890), "System Administrator with full access", true, "Admin", "ADMIN", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3890) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "251eda3d-694b-4b6e-99af-1ebfbea3609d", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3920), "Default user role with basic permissions", true, "User", "USER", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3920) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "fe77c512-00a8-4c98-9a72-b6dd59b26a0f", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3930), "Generic role to assign dynamic permissions", false, "GenericRole", "GENERICROLE", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(3930) }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "DisplayName", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Users", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), "Yeni kullanıcı oluşturabilir", "Kullanıcı Oluşturma", "Users.Create", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Users", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), "Kullanıcıları görüntüleyebilir", "Kullanıcı Görüntüleme", "Users.Read", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Users", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000), "Kullanıcı bilgilerini güncelleyebilir", "Kullanıcı Güncelleme", "Users.Update", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4000) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Users", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Kullanıcıları silebilir", "Kullanıcı Silme", "Users.Delete", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Roles", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Yeni rol oluşturabilir", "Rol Oluşturma", "Roles.Create", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Roles", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Rolleri görüntüleyebilir", "Rol Görüntüleme", "Roles.Read", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Roles", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Rol bilgilerini güncelleyebilir", "Rol Güncelleme", "Roles.Update", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Roles", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Rolleri silebilir", "Rol Silme", "Roles.Delete", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Permissions", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010), "Yeni yetki tanımlayabilir", "Yetki Oluşturma", "Permissions.Create", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "Permissions", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "Yetkileri görüntüleyebilir", "Yetki Görüntüleme", "Permissions.Read", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "Permissions", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "Yetki bilgilerini güncelleyebilir", "Yetki Güncelleme", "Permissions.Update", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "Permissions", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "Yetkileri silebilir", "Yetki Silme", "Permissions.Delete", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "Posts", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "Yeni içerik oluşturabilir", "İçerik Oluşturma", "Posts.Create", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "Posts", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "İçerik güncelleyebilir", "İçerik Güncelleme", "Posts.Update", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "Posts", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020), "İçerik silebilir", "İçerik Silme", "Posts.Delete", new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4020) }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "GrantedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4070) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4070) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4080) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 19, 21, 41, 0, 985, DateTimeKind.Utc).AddTicks(4090) }
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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");
        }

        /// <inheritdoc />
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
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
