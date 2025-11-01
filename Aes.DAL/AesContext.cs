using Microsoft.EntityFrameworkCore;
using Aes.DAL.Models;

namespace Aes.DAL
{
    public class AesContext : DbContext
    {
        public DbSet<FacilityObject> FacilityObjects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=aesinfo.db");
        }
    }
}
