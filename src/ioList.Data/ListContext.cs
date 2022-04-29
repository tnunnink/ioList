using System.Linq;
using ioList.Domain;
using Microsoft.EntityFrameworkCore;

namespace ioList.Data
{
    public class ListContext : DbContext
    {
        public ListContext(DbContextOptions<ListContext> options) : base(options)
        {
        }
        
        internal DbSet<List> Lists { get; set; }
        public List List => Lists.FirstOrDefault();
        public DbSet<Card> Cards { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListContext).Assembly);
        }
    }
}