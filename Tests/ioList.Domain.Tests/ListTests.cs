using System;
using FluentAssertions;
using NUnit.Framework;

namespace ioList.Domain.Tests
{
    [TestFixture]
    public class ListTests
    {
        [Test]
        public void New_ValidParameters_ShouldNotBeNull()
        {
            var list = new List("ListName", @"C:\", "Source", "This is a test");

            list.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new List(null!, @"C:\PathToFile", "Source")).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Path_ShouldBeDirectoryAndName()
        {
            var list = new List("Name", @"C:\PathToFile", "Source");

            list.FileName.Should().Be(@$"{list.Directory}\{list.Name}.db");
        }
    }
}