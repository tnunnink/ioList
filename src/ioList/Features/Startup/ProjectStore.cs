using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ioList.Entities;

namespace ioList.Features.Startup
{
    public class RecentProjectsStore
    {
        private readonly XDocument _document;
        
        public RecentProjectsStore()
        {
            _document = XDocument.Load("");
        }

        public IEnumerable<ProjectFile> GetProjects()
        {
            return _document.Descendants("Project").Select(p => new ProjectFile
            {
                Name = p.Attribute("Name")?.Value,
                Location = p.Attribute("Location")?.Value,
            });
        }
    }
}