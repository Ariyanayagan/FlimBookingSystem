using Flim.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        ISeatRepository SeatRepository { get; }
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveAsync();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();

        Task DisposeTransactionAsync();
    }
}
