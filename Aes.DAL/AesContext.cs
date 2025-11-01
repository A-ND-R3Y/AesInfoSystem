using Microsoft.EntityFrameworkCore;
using Aes.DAL.Models;

namespace Aes.DAL
{
    public class AesContext : DbContext
    {
        public DbSet<FacilityObject> FacilityObjects { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite DB файл у корені проекту Aes.DAL
            optionsBuilder.UseSqlite("Data Source=AesInfo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilityObject>(b =>
            {
                b.HasKey(f => f.Id);
                b.Property(f => f.Name).IsRequired().HasMaxLength(200);
                b.Property(f => f.Type).HasMaxLength(100);
                b.Property(f => f.Location).HasMaxLength(150);
            });

            modelBuilder.Entity<Employee>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            });
        }
    }
}
