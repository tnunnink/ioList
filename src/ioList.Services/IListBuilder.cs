using System.Threading;
using System.Threading.Tasks;
using ioList.Domain;

namespace ioList.Services
{
    public interface IListBuilder
    {
        public Task BuildAsync(List list, CancellationToken token);
    }
}