using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ioList.Domain;

namespace ioList.Services
{
    public class ListFileService : IListFileService
    {
        private const string DataFile = @"ioList\Lists.xml";

        private readonly string _fileName =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DataFile);

        public ListFileService()
        {
            if (!File.Exists(_fileName))
                GenerateDocument(_fileName);
        }

        public IEnumerable<ListFile> GetAll()
        {
            var document = XDocument.Load(_fileName);
            return document.Descendants(nameof(ListFile)).Select(e => new ListFile(e.Value));
        }


        public void Add(ListFile listFile)
        {
            var document = XDocument.Load(_fileName);

            if (Exists(listFile.FullName))
                return;

            document.Root?.Add(new XElement(nameof(ListFile), listFile.FullName));

            document.Save(_fileName);
        }

        private bool Exists(string fullName)
        {
            var document = XDocument.Load(_fileName);
            
            return document.Descendants(nameof(ListFile))
                .Any(e => string.Equals(e.Value, fullName, StringComparison.OrdinalIgnoreCase));
        }

        public void Remove(ListFile listFile)
        {
            var document = XDocument.Load(_fileName);
            
            var element = document.Descendants(nameof(ListFile))
                .FirstOrDefault(e => e.Value == listFile.FullName);

            element?.Remove();

            document.Save(_fileName);
        }

        private static void GenerateDocument(string fileName)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement("Lists");
            var document = new XDocument(declaration, root);
            document.Save(fileName);
        }
    }
}