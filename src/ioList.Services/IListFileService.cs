using System.Collections.Generic;
using ioList.Entities;

namespace ioList.Services
{
    public interface IListFileService
    {
        IEnumerable<ListFile> GetAll();
        void Add(ListFile listFile);
        void Remove(ListFile listFile); 
    }
    
    
}