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
        
        private DbSet<ListInfo> ListInfo { get; set; }
        public ListInfo List => ListInfo.FirstOrDefault();
        public DbSet<Card> Cards { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListContext).Assembly);
        }
    }
}