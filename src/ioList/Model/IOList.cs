using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp;

namespace ioList.Model
{
    public class IOList
    {
        private const string Extension = ".list";
        private readonly XDocument _document;

        public IOList(string name, string path, string comment, L5XContext source)
        {
            File = new ListFile(name, path);
            _document = GenerateNewList(name, path, comment, source);
        }

        public IOList(ListFile file)
        {
            File = file;
            _document = XDocument.Load(file.ListPath);
        }

        public ListFile File { get; }
        public string Name => _document.Root?.Attribute(nameof(Name))?.Value;
        public string Comment => _document.Root?.Attribute(nameof(Comment))?.Value;
        public string CreatedBy => _document.Root?.Attribute(nameof(CreatedBy))?.Value;

        public DateTime CreatedOn =>
            DateTime.TryParse(_document.Root?.Attribute(nameof(CreatedOn))?.Value, out var dateTime)
                ? dateTime
                : default;

        public List<IOTag> Tags { get; }
        public L5XContext Source { get; }

        public void Save()
        {
            _document.Save(File.ListPath);
        }

        private static XDocument GenerateNewList(string name, string path, string comment, L5XContext source)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var element = new XElement(nameof(IOList));

            element.Add(new XAttribute(nameof(Name), name));
            element.Add(new XAttribute(nameof(Comment), comment));
            element.Add(new XAttribute(nameof(CreatedBy), Environment.UserName));
            element.Add(new XAttribute(nameof(CreatedOn), DateTime.Now));
            element.Add(new XElement(nameof(Source), source.ToString()));

            var document = new XDocument(declaration, element);
            document.Save(path);

            return document;
        }
    }
}