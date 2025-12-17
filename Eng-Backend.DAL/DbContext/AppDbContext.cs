using Eng_Backend.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.DAL.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentRole> DocumentRoles { get; set; }

    // Problem/Task Management
    public DbSet<Problem> Problems { get; set; }
    public DbSet<ProblemAssignment> ProblemAssignments { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<SolutionFile> SolutionFiles { get; set; }

    // Quiz/Gamification
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<UserScore> UserScores { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure RolePermission composite key
        builder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // Configure relationships
        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Permission unique index on Name
        builder.Entity<Permission>()
            .HasIndex(p => p.Name)
            .IsUnique();

        // Configure Document entity
        builder.Entity<Document>()
            .HasOne(d => d.CreatedByUser)
            .WithMany()
            .HasForeignKey(d => d.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure DocumentRole composite key
        builder.Entity<DocumentRole>()
            .HasKey(dr => new { dr.DocumentId, dr.RoleId });

        builder.Entity<DocumentRole>()
            .HasOne(dr => dr.Document)
            .WithMany(d => d.DocumentRoles)
            .HasForeignKey(dr => dr.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DocumentRole>()
            .HasOne(dr => dr.Role)
            .WithMany()
            .HasForeignKey(dr => dr.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============ Problem/Task Configuration ============

        // Problem
        builder.Entity<Problem>()
            .HasOne(p => p.CreatedByUser)
            .WithMany()
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ProblemAssignment
        builder.Entity<ProblemAssignment>()
            .HasOne(pa => pa.Problem)
            .WithMany(p => p.Assignments)
            .HasForeignKey(pa => pa.ProblemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProblemAssignment>()
            .HasOne(pa => pa.AssignedToUser)
            .WithMany()
            .HasForeignKey(pa => pa.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProblemAssignment>()
            .HasOne(pa => pa.AssignedByUser)
            .WithMany()
            .HasForeignKey(pa => pa.AssignedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Solution
        builder.Entity<Solution>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Solutions)
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Solution>()
            .HasOne(s => s.ReviewedByUser)
            .WithMany()
            .HasForeignKey(s => s.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // SolutionFile
        builder.Entity<SolutionFile>()
            .HasOne(sf => sf.Solution)
            .WithMany(s => s.Files)
            .HasForeignKey(sf => sf.SolutionId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============ Quiz/Gamification Configuration ============

        // Question
        builder.Entity<Question>()
            .HasOne(q => q.Document)
            .WithMany()
            .HasForeignKey(q => q.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        // UserAnswer
        builder.Entity<UserAnswer>()
            .HasOne(ua => ua.Question)
            .WithMany(q => q.UserAnswers)
            .HasForeignKey(ua => ua.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserAnswer>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Prevent duplicate answers - one answer per user per question
        builder.Entity<UserAnswer>()
            .HasIndex(ua => new { ua.UserId, ua.QuestionId })
            .IsUnique();

        // UserScore - one score record per user
        builder.Entity<UserScore>()
            .HasOne(us => us.User)
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserScore>()
            .HasIndex(us => us.UserId)
            .IsUnique();

        // Seed Data
        SeedRoles(builder);
        SeedPermissions(builder);
        SeedAdminRolePermissions(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var userRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var genericRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "System Administrator with full access",
                IsSystemRole = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER",
                Description = "Default user role with basic permissions",
                IsSystemRole = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = genericRoleId,
                Name = "GenericRole",
                NormalizedName = "GENERICROLE",
                Description = "Generic role to assign dynamic permissions",
                IsSystemRole = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }

    private void SeedPermissions(ModelBuilder builder)
    {
        var permissions = new List<Permission>
        {
            // Users Management
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "Users.Create", DisplayName = "Kullanıcı Oluşturma", Category = "Users", Description = "Yeni kullanıcı oluşturabilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Users.Read", DisplayName = "Kullanıcı Görüntüleme", Category = "Users", Description = "Kullanıcıları görüntüleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Users.Update", DisplayName = "Kullanıcı Güncelleme", Category = "Users", Description = "Kullanıcı bilgilerini güncelleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "Users.Delete", DisplayName = "Kullanıcı Silme", Category = "Users", Description = "Kullanıcıları silebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },

            // Roles Management
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Name = "Roles.Create", DisplayName = "Rol Oluşturma", Category = "Roles", Description = "Yeni rol oluşturabilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Name = "Roles.Read", DisplayName = "Rol Görüntüleme", Category = "Roles", Description = "Rolleri görüntüleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Name = "Roles.Update", DisplayName = "Rol Güncelleme", Category = "Roles", Description = "Rol bilgilerini güncelleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Name = "Roles.Delete", DisplayName = "Rol Silme", Category = "Roles", Description = "Rolleri silebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },

            // Permissions Management
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Name = "Permissions.Create", DisplayName = "Yetki Oluşturma", Category = "Permissions", Description = "Yeni yetki tanımlayabilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), Name = "Permissions.Read", DisplayName = "Yetki Görüntüleme", Category = "Permissions", Description = "Yetkileri görüntüleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000011"), Name = "Permissions.Update", DisplayName = "Yetki Güncelleme", Category = "Permissions", Description = "Yetki bilgilerini güncelleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000012"), Name = "Permissions.Delete", DisplayName = "Yetki Silme", Category = "Permissions", Description = "Yetkileri silebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },

            // Posts Management (Example)
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000013"), Name = "Posts.Create", DisplayName = "İçerik Oluşturma", Category = "Posts", Description = "Yeni içerik oluşturabilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000014"), Name = "Posts.Update", DisplayName = "İçerik Güncelleme", Category = "Posts", Description = "İçerik güncelleyebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Permission { Id = Guid.Parse("10000000-0000-0000-0000-000000000015"), Name = "Posts.Delete", DisplayName = "İçerik Silme", Category = "Posts", Description = "İçerik silebilir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };

        builder.Entity<Permission>().HasData(permissions);
    }

    private void SeedAdminRolePermissions(ModelBuilder builder)
    {
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Admin gets all permissions
        var adminPermissions = new List<RolePermission>();
        for (int i = 1; i <= 15; i++)
        {
            adminPermissions.Add(new RolePermission
            {
                RoleId = adminRoleId,
                PermissionId = Guid.Parse($"10000000-0000-0000-0000-0000000000{i:D2}"),
                GrantedAt = DateTime.UtcNow
            });
        }

        builder.Entity<RolePermission>().HasData(adminPermissions);
    }
}