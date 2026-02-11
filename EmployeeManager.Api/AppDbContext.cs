using EmployeeManager.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using Task = EmployeeManager.Shared.Task;

namespace EmployeeManager.Api
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Employee> Employees { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PassWordHash).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(50);

                // Relation un-à-plusieurs
                entity.HasMany(u => u.Tasks)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Description).HasMaxLength(1000);
                entity.Property(t => t.State).HasMaxLength(50);
                entity.Property(t => t.Created).HasDefaultValueSql("NOW()");
                entity.Property(t => t.Updated).HasDefaultValueSql("NOW()");
                entity.Property(t => t.effortEstimation);
                entity.Property(t => t.priority);
                entity.Property(t => t.progress);
                entity.Property(t => t.effortEstimation);

            });
        }


    }
}
