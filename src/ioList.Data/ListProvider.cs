using ioList.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace ioList.Data
{
    public class ListProvider : IListProvider
    {
        public IListRepository Connect(string listPath)
        {
            var connection = new SqliteConnectionStringBuilder
            {
                DataSource = listPath
            };
            
            var options = new DbContextOptionsBuilder<ListContext>().UseSqlite(connection.ConnectionString).Options;

            var context = new ListContext(options);

            return new ListRepository(context);
        }
    }
}