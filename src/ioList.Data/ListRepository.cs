using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ioList.Domain;
using ioList.Services;
using Microsoft.EntityFrameworkCore;

namespace ioList.Data
{
    public class ListRepository : IListRepository
    {
        private readonly ListContext _context;

        public ListRepository(ListContext context)
        {
            _context = context;
        }

        public async Task<List> GetList() => await _context.Lists.FirstOrDefaultAsync();
        public Task<List<Module>> GetModules()
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> GetTags() => _context.Tags.ToListAsync();
        public Task AddModule(Module module)
        {
            throw new NotImplementedException();
        }

        public async Task AddModules(IEnumerable<Module> modules)
        {
            
        }

        Task IListRepository.AddTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task AddTags(IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public void RenameList(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty.");
            
            _context.List.Name = name;

            _context.SaveChanges();
        }

        public void AddTag(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}