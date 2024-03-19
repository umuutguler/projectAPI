using System.Reflection;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ReservationInfo> ReservationInfos { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReservationInfo>()
                .ToTable("Reservations")
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
              .ToTable("Users")
              .HasKey(x => x.Id);

            modelBuilder.Entity<ReservationInfo>()
              .HasOne(x => x.User)
              .WithMany(x => x.ReservationInfos)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}