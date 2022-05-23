using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using List = ioList.Domain.List;
using NUnit.Framework;

namespace ioList.Data.Tests
{
    [TestFixture]
    public class ListBuilderTests
    {
        private static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.L5X");
        
        [Test]
        public async Task Build_Default_ShouldWork()
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"TestFiles");

            var list = new List
            {
                Name = "Test",
                Directory = path,
                Comment = "This is a test"
            };

            var builder = new ListBuilder();
            
            await builder.Build(list, CancellationToken.None);

            FileAssert.Exists(Path.Combine(path, "Test.db"));
            
            File.Delete(list.FullPath);
            
            FileAssert.DoesNotExist(Path.Combine(path, "Test.db"));
        }
    }
}