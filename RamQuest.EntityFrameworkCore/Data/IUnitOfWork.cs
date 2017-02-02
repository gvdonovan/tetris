using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RamQuest.EntityFrameworkCore.Data
{
    public interface IUnitOfWork
    {
        TContext Context<TContext>() where TContext : DbContext;
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        void SaveChanges();
    }

    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {

        private DbContext _context;
        private bool _disposed;

        public UnitOfWork()
        {
            _context = new TContext();
        }

        public T Context<T>() where T : DbContext => _context as T;

        TContext1 IUnitOfWork.Context<TContext1>()
        {
            throw new NotImplementedException();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public EntityEntry<T> Entry<T>(T entity) where T : class
        {
            return _context.Entry<T>(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
