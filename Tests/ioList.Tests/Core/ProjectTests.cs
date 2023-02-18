using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using ioList.Core;
using NUnit.Framework;

namespace ioList.Tests.Core
{
    [TestFixture]
    public class ProjectTests
    {
        private static readonly string MyProject = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ioList\Projects\MyProject.db");

        private IFileSystem _fileSystem;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\someProject.db", new MockFileData("Testing is meh.") },
                { @"c:\demo\File.txt", new MockFileData("some js") },
                { @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            });
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var project = new Project(_fileSystem);

            project.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedPropertyValues()
        {
            var project = new Project(_fileSystem);

            project.Name.Should().Be("MyProject");
            project.Location.Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"ioList\Projects"));
            project.FileName.Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"ioList\Projects\MyProject.db"));
            project.Exists.Should().BeFalse();
        }

        [Test]
        public void New_FileName_ShouldNotBeNull()
        {
            var project = new Project(_fileSystem);

            project.Name.Should().Be("MyProject");
            project.Location.Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"ioList\Projects"));
            project.FileName.Should().Be(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                @"ioList\Projects\MyProject.db"));
            project.Exists.Should().BeFalse();
        }

        [Test]
        public void FileWatcher()
        {
            var project = new Project(_fileSystem);
        }
    }
}