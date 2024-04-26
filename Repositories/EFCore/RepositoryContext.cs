using System.Reflection;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Repositories.EFCore.Config;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ReservationInfo> ReservationInfos { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToCollection("Departments");
            modelBuilder.Entity<ReservationInfo>().ToCollection("ReservationInfos");
            modelBuilder.Entity<Chair>().ToCollection("Chairs");
            modelBuilder.Entity<Table>().ToCollection("Tables");
            modelBuilder.Entity<User>().ToCollection("Users");

            modelBuilder.ApplyConfiguration(new ChairConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new TableConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}