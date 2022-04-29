using System;
using System.IO;
using L5Sharp;
using List = ioList.Domain.List;
using NUnit.Framework;

namespace ioList.Data.Tests
{
    [TestFixture]
    public class ListBuilderTests
    {
        private static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.L5X");
        
        [Test]
        public void Build_Default_ShouldWork()
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"TestFiles");
            var context = LogixContext.Load(L5X);

            var list = new List("Test", path, context.ToString(), "This is a test");
            
            var builder = new ListBuilder();

            builder.Build(list);
            
            FileAssert.Exists(Path.Combine(path, "Test.db"));
        }
    }
}