using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.EFDataAccess;
using MongoDB.Bson;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class GenericRepository<T, TKey> : IRepository<T, TKey>
        where T : Entity
    {
        protected readonly ApplicationContext DbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>().Where(e => !e.Deleted).ToList();
        }

        public virtual T Get(TKey id)
        {
            T entity = DbContext.Set<T>().Find(id);
            return entity.Deleted ? null : entity;
        }

        public virtual IEnumerable<T> Find(IPipeline<T> pipeline)
        {
            IQueryable<T> notDeletedEntities = DbContext.Set<T>().Where(e => !e.Deleted);
            return pipeline.Process(notDeletedEntities).ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(e => !e.Deleted).Where(expression).ToList();
        }

        public T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(e => !e.Deleted).SingleOrDefault(expression);
        }

        public T SingleOrDefaultDeleted(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(e => e.Deleted).SingleOrDefault(expression);
        }

        public virtual IEnumerable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            return DbContext.Set<T>().Where(e => !e.Deleted).Select(selector).ToList();
        }

        public virtual void Create(T item)
        {
            DbContext.Set<T>().Add(item);
        }

        public virtual void Update(T item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public virtual void Delete(T item)
        {
            item.Deleted = true;
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? DbContext.Set<T>().Count(e => !e.Deleted) : DbContext.Set<T>().Where(e => !e.Deleted).Count(predicate);
        }

        public string GetOldEntityJson(T entity)
        {
            DbPropertyValues current = DbContext.Entry(entity).CurrentValues.Clone();
            DbContext.Entry(entity).Reload();
            string oldEntity = entity.ToJson();

            DbContext.Entry(entity).CurrentValues.SetValues(current);
            return oldEntity;
        }

        public T GetDeletedEntity(TKey id)
        {
            T entity = DbContext.Set<T>().Find(id);
            return entity.Deleted ? entity : null;
        }

        public IEnumerable<T> FindDeletedEntities(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(e => e.Deleted).Where(expression).ToList();
        }
    }
}