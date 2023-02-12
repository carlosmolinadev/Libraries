using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }

        Task BeginAsync();
        void CommitAsync();
        void Dispose();
    }
}
