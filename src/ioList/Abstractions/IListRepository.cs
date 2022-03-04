using System.Collections.Generic;
using ioList.Model;

namespace ioList.Abstractions
{
    public interface IListRepository
    {
        public IEnumerable<IoList> GetFor(string userName);
    }
}