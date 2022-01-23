using System.Collections.Generic;

namespace ioList.Module.IoSelection.Services
{
    public interface ICardDataService
    {
        void Load(string fileName);
        IEnumerable<Domain.Module> GetAll();
        Domain.Module Get();
    }
}