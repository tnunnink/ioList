using System.Collections.Generic;
using ioList.Module.LoadFile.Model;

namespace ioList.Module.LoadFile.Services
{
    public interface IRecentFileDataService
    {
        IEnumerable<RecentFile> GetAll();
        RecentFile Get(string path);
        void Add(RecentFile recentFile);
        void Remove(RecentFile recentFile);
    }
}