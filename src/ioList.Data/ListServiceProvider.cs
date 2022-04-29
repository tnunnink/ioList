using ioList.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace ioList.Data
{
    public class ListServiceProvider : IListServiceProvider
    {
        public IListService Connect(string listPath)
        {
            var connection = new SqliteConnectionStringBuilder
            {
                DataSource = listPath
            };
            
            var options = new DbContextOptionsBuilder<ListContext>().UseSqlite(connection.ConnectionString).Options;

            var context = new ListContext(options);

            return new ListService(context);
        }
    }
}