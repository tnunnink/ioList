using System.Collections.Generic;
using ioList.Module.IoSelection.Model;

namespace ioList.Module.IoSelection.Services
{
    public interface IModuleDataService
    {
        void Load(string fileName);
        IEnumerable<LogixModule> GetAll();
        LogixModule Get();
    }
}