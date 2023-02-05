using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ioList.Entities;

namespace ioList.Services
{
    public interface IListRepository : IDisposable
    {
        Task<List> GetList();
        Task<List<Module>> GetModules();
        Task<List<Tag>> GetTags();
        Task AddModule(Module module);
        Task AddModules(IEnumerable<Module> modules);
        Task AddTag(Tag tag);
        Task AddTags(IEnumerable<Tag> tags);
        void RenameList(string name);
    }
}