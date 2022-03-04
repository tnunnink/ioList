using ioList.Model;
using Microsoft.EntityFrameworkCore;

namespace ioList.Services
{
    public class ListContext : DbContext
    {
        public DbSet<IoList> Lists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListContext).Assembly);
        }
    }
}