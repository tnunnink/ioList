using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ioList.Abstractions;
using ioList.Model;

namespace ioList.Services
{
    public class ListFileService : IListFileService
    {
        private const string ListFiles = "ListFiles";
        private const string ListFile = "ListFile";
        private const string DataFile = @"ioList\ListFiles.xml";

        private readonly string _fileName =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DataFile);

        private readonly XDocument _document;

        public ListFileService()
        {
            var fileInfo = new FileInfo(_fileName);

            if (!fileInfo.Exists)
                GenerateDocument(_fileName);
            
            _document = XDocument.Load(_fileName);
        }

        public void Clear()
        {
            _document.Root?.RemoveNodes();
            _document.Save(_fileName);
        }

        public IEnumerable<ListFile> GetAll() =>
            _document.Descendants(ListFile).Select(e => new ListFile(e));

        public void Add(ListFile listFile)
        {
            if (Exists(listFile.ListPath))
                return;
            
            _document.Root?.Add(listFile.Serialize());

            _document.Save(_fileName);
        }

        public void Remove(ListFile listFile)
        {
            var element = _document.Descendants(ListFile)
                .FirstOrDefault(e => e.Value == listFile.ListPath);

            element?.Remove();

            _document.Save(_fileName);
        }

        private bool Exists(string fullName) =>
            _document.Descendants(ListFile)
                .Any(e => string.Equals(e.Value, fullName, StringComparison.OrdinalIgnoreCase));

        private static void GenerateDocument(string fileName)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement(ListFiles);
            var document = new XDocument(declaration, root);
            document.Save(fileName);
        }
    }
}