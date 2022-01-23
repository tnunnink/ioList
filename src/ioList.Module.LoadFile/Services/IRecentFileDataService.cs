using System.Collections.Generic;
using ioList.Domain;
using ioList.Module.LoadFile.ViewModels;

namespace ioList.Module.LoadFile.Services
{
    public interface IRecentFileDataService
    {
        void Clear();
        IEnumerable<RecentFile> GetAll();
        void Add(RecentFile fileViewModel);
        void Remove(RecentFile fileViewModel);
    }
}