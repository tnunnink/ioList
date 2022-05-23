using System.Threading;
using System.Threading.Tasks;
using ioList.Domain;

namespace ioList.Services
{
    public interface IListBuilder
    {
        public Task Build(List list, CancellationToken token);
    }
}