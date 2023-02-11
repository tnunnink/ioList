using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ioList.Shared.Services;

namespace ioList.Data.ProjectDb
{
    public class ProjectConnection : IConnectionProvider
    {
        public Task<IDbConnection> Connect(string connectionString, CancellationToken token)
        {
            //perform connection.
            //verify databse
            //migrate if necessary
            //return db connection
            throw new NotImplementedException();
        }
    }
}