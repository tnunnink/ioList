using System.Collections.Generic;
using ioList.Module.LoadFile.Model;

namespace ioList.Module.LoadFile.Services
{
    public interface IRecentFileDataService
    {
        void Clear();
        IEnumerable<RecentFile> GetAll();
        void Add(RecentFile recentFile);
        void Remove(RecentFile recentFile);
    }
}