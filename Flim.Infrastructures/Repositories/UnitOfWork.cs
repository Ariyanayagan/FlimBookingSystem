using Flim.Application.Services;
using Flim.Domain.Interfaces;
using Flim.Domain.Shared;
using Flim.Infrastructures.Data;
using Flim.Infrastructures.Interfaces;
using Flim.Infrastructures.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Flim.Infrastructures.Repositories
{
    /// <summary>
    /// Implementation of UnitOfwork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _context;
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private readonly IServiceProvider _serviceProvider;
        private ISeatRepository _seatRepository;

        public UnitOfWork(BookingDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ISeatRepository SeatRepository
        {
            get
            {
                return _seatRepository ??= _serviceProvider.GetService<ISeatRepository>();
            }
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return _serviceProvider.GetService<IGenericRepository<TEntity>>();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async void CommitTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposeTransactionAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


    }
}
