using System;
using System.IO;

namespace ioList.Entities
{
    public class ProjectFile
    {
        private static readonly string DefaultLocation =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ioList\Projects");

        /// <summary>
        /// Creates a default project file instance with empty name and default location values.
        /// </summary>
        public ProjectFile()
        {
        }

        /// <summary>
        /// Creates a new project entity with the specified name and default project location.
        /// </summary>
        /// <param name="name"></param>
        public ProjectFile(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Creates a new project file entity from the specified file name.
        /// </summary>
        /// <param name="fileName">The full path to the fill name including extension.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><c>fileName</c> is null or empty or does not have correct file name.
        /// The file may not exist</exception>
        public static ProjectFile FromFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename can not be null or empty.");

            var extension = Path.GetExtension(fileName);
            if (extension != ".db")
                throw new ArgumentException(
                    "File extension is not valid. Must be .db file extension to represent a valid project file");

            var name = Path.GetFileNameWithoutExtension(fileName);
            var location = Path.GetDirectoryName(fileName);
            return new ProjectFile { Name = name, Location = location };
        }

        public string Name { get; set; } = "MyProject";
        public string Location { get; set; } = DefaultLocation;

        public string FileName() => Path.Combine(Location, Name, ".db");
    }
}