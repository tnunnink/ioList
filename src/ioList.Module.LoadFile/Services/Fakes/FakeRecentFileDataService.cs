using System.Collections.Generic;
using ioList.Module.LoadFile.Model;

namespace ioList.Module.LoadFile.Services.Fakes
{
    public class FakeRecentFileDataService : IRecentFileDataService
    {
        private readonly List<RecentFile> _files;
        public FakeRecentFileDataService()
        {
            _files = new List<RecentFile>
            {
                new("MyController.L5X"),
                new("SomeExportFile.L5X"),
                new("ModuleExport.L5X"),
                new("MyProject.L5X")
            };
        }

        public void Clear() => _files.Clear();

        public IEnumerable<RecentFile> GetAll() => _files;

        public void Add(RecentFile recentFile) => _files.Add(recentFile);

        public void Remove(RecentFile recentFile) => _files.Remove(recentFile);
    }
}