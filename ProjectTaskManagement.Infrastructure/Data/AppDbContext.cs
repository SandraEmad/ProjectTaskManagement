using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectTask> Tasks => Set<ProjectTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            // User
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.UserName).HasMaxLength(100);
                entity.Property(u => u.PasswordHash).HasMaxLength(500);
                entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();  
                entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();   
            });

            // Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Projects)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ProjectTask
            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.Property(t => t.Title).HasMaxLength(200).IsRequired();     
                entity.Property(t => t.Description).HasMaxLength(1000);
                entity.Property(t => t.Status).HasConversion<string>();            
                entity.Property(t => t.Priority).HasConversion<string>();         
                entity.HasOne(t => t.Project)
                      .WithMany(p => p.Tasks)
                      .HasForeignKey(t => t.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}