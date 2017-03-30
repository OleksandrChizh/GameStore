using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.Domain.Interfaces
{
    public interface IRepository<T, in TKey> where T : class 
    {
        IEnumerable<T> GetAll();

        T Get(TKey id);

        IEnumerable<T> Find(IPipeline<T> pipline);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        T SingleOrDefault(Expression<Func<T, bool>> expression);

        T SingleOrDefaultDeleted(Expression<Func<T, bool>> expression);

        void Create(T item);

        void Update(T item);

        void Delete(T item);

        int Count(Expression<Func<T, bool>> predicate = null);

        string GetOldEntityJson(T entity);

        T GetDeletedEntity(TKey id);

        IEnumerable<T> FindDeletedEntities(Expression<Func<T, bool>> expression);
    }
}
