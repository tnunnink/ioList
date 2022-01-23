using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ioList.Domain;
using ioList.Module.LoadFile.ViewModels;

namespace ioList.Module.LoadFile.Services
{
    public class RecentFileDataService : IRecentFileDataService
    {
        private const string RecentFile = "RecentFile";
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

        public void Clear()
        {
            _document.Root?.RemoveNodes();
            _document.Save(_fileName);
        }

        public IEnumerable<RecentFile> GetAll() =>
            _document.Descendants(RecentFile).Select(e => new RecentFile(e.Value));

        public void Add(RecentFile fileViewModel)
        {
            if (Exists(fileViewModel.FullPath))
                return;

            var element = new XElement(RecentFile, fileViewModel.FullPath);

            _document.Root?.Add(element);

            _document.Save(_fileName);
        }

        public void Remove(RecentFile fileViewModel)
        {
            var element = _document.Descendants(RecentFile)
                .FirstOrDefault(e => e.Value == fileViewModel.FullPath);

            element?.Remove();

            _document.Save(_fileName);
        }

        private bool Exists(string fullName) =>
            _document.Descendants(RecentFile)
                .Any(e => string.Equals(e.Value, fullName, StringComparison.OrdinalIgnoreCase));

        private static void GenerateDocument(string fileName)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement("RecentFiles");
            var document = new XDocument(declaration, root);
            document.Save(fileName);
        }
    }
}