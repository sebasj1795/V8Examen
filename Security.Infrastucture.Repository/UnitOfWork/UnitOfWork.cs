using Security.Domain.Interfaces.IUnitOfWork;
using Security.Infrastructure.DataModel.Context;
using System;
using System.Threading.Tasks;

namespace Security.Infrastucture.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext _mainContext;
        private bool _disposed;

        //MainContext para sobreescribir metodo savechangeasync
        public UnitOfWork(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task SaveChangesAsync()
        {
            return _mainContext.SaveChangesAsync();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task BeginTransactionAsync()
        {
            //_disposed = false;
            await _mainContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _mainContext.SaveChangesAsync();
            await _mainContext.Database.CommitTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _mainContext.Dispose();
            _disposed = true;
        }
    }
}
