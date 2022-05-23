using System.Threading;
using System.Threading.Tasks;
using ioList.Domain;
using ioList.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ioList.Data
{
    public class ListBuilder : IListBuilder
    {
        public async Task Build(List list, CancellationToken token)
        {
            await Task.Run(async () =>
            {
                var connection = new SqliteConnectionStringBuilder { DataSource = list.FullPath };
                var options = new DbContextOptionsBuilder<ListContext>().UseSqlite(connection.ConnectionString).Options;
                
                await using var context = new ListContext(options);
                await context.Database.EnsureCreatedAsync(token);
                await context.Lists.AddAsync(list, token);
                await context.SaveChangesAsync(token);
            }, token);
        }
    }
}