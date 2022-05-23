using System;
using System.IO;
using System.Threading;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using List = ioList.Domain.List;

namespace ioList.Data.Tests
{
    [TestFixture]
    public class ListContextTests
    {
        private DbContextOptions<ListContext> _options;

        [OneTimeSetUp]
        public void Setup()
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"TestFiles");

            var list = new List
            {
                Name = "Test",
                Directory = path,
                Comment = "This is a test"
            };

            var builder = new ListBuilder();
            
            builder.Build(list, CancellationToken.None);
            
            var connection = new SqliteConnectionStringBuilder { DataSource = list.FullPath };
            _options = new DbContextOptionsBuilder<ListContext>().UseSqlite(connection.ConnectionString).Options;
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            using var context = new ListContext(_options);
            context.Database.EnsureDeleted();
        }
        
        
    }
}