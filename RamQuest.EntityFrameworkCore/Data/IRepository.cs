using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RamQuest.Core.Model;

namespace RamQuest.EntityFrameworkCore.Data
{
    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        TEntity Get(TId id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
    }

    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        internal DbSet<TEntity> DbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            DbSet = UnitOfWork.Set<TEntity>();
        }

        private IUnitOfWork UnitOfWork { get; }

        public TEntity Get(TId id)
        {
            return UnitOfWork.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var result = DbSet.AsNoTracking().Where(predicate);
            return result.ToList();
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(TId id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}