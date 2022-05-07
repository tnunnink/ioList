using System.IO;
using ioList.Domain;
using ioList.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ioList.Data
{
    public class ListBuilder : IListBuilder
    {
        public void Build(List list)
        {
            var path = Path.Combine(list.Directory, $"{list.Name}.db");
            var connection = new SqliteConnectionStringBuilder { DataSource = path };
            var options = new DbContextOptionsBuilder<ListContext>().UseSqlite(connection.ConnectionString).Options;
            
            using var context = new ListContext(options);
            context.Database.EnsureCreated();
            context.Lists.Add(list);
            context.SaveChanges();
        }
    }
}