using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ReadersApi.Providers
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }
        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }
        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}