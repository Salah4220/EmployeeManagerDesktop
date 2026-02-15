using EmployeeManager.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeManager.Shared.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PassWordHash).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(50);

                entity.HasMany(u => u.Tasks)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<EmployeeManager.Shared.Task>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Description).HasMaxLength(1000);
                entity.Property(t => t.State).HasMaxLength(50);

                // If using SQL Server, use "GETDATE()". If PostgreSQL, use "now()"
                entity.Property(t => t.Created).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.Updated).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}