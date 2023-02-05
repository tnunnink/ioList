using System.IO;

namespace ioList.Entities
{
    public class ProjectFile
    {
        private const string DefaultLocation = "";
        
        public string Name { get; set; } = "New Project";
        public string Location { get; set; } = DefaultLocation;

        public void FileName() => Path.Combine(Location, Name, ".db");
    }
}