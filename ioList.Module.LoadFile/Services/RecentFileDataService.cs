using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ioList.Module.LoadFile.Model;

namespace ioList.Module.LoadFile.Services
{
    public class RecentFileDataService : IRecentFileDataService
    {
        private const string DataFile = @"ioList\RecentFiles.xml";

        private readonly string _fileName =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DataFile);

        private readonly XDocument _document;

        public RecentFileDataService()
        {
            var fileInfo = new FileInfo(_fileName);

            //var directoryInfo = new DirectoryInfo();

            if (!fileInfo.Exists)
            {
                GenerateDocument(_fileName);
            }


            _document = XDocument.Load(_fileName);
        }

        public IEnumerable<RecentFile> GetAll() =>
            _document.Descendants(nameof(RecentFile)).Select(e => new RecentFile(e.Value));

        public RecentFile Get(string path) =>
            new(_document.Descendants(nameof(RecentFile)).FirstOrDefault(e => e.Value == path)?.Value);

        public bool Exists(string fullName) =>
            _document.Descendants(nameof(RecentFile))
                .Any(e => string.Equals(e.Value, fullName, StringComparison.OrdinalIgnoreCase));

        public void Add(RecentFile recentFile)
        {
            if (Exists(recentFile.FullName))
                return;

            var element = new XElement(nameof(RecentFile), recentFile.FullName);

            _document.Root?.Add(element);

            _document.Save(_fileName);
        }

        public void Remove(RecentFile recentFile)
        {
            var element = _document.Descendants(nameof(RecentFile)).FirstOrDefault(e => e.Value == recentFile.FullName);

            element?.Remove();

            _document.Save(_fileName);
        }

        private static void GenerateDocument(string fileName)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement("RecentFiles");
            var document = new XDocument(declaration, root);
            document.Save(fileName);
        }
    }
}