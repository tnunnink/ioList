using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ioList.Shared.Services
{
    public interface IConnectionProvider
    {
        Task<IDbConnection> Connect(string connectionString, CancellationToken token);
    }
}