using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Bson;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class RepositoryDecorator<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        private readonly IRepository<T, TKey> _efRepository;
        private readonly IMongoRepository<T> _mongoRepository;

        public RepositoryDecorator(
            IRepository<T, TKey> efRepository,
            IMongoRepository<T> mongoRepository)
        {
            _efRepository = efRepository;
            _mongoRepository = mongoRepository;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return _efRepository.Count(predicate);
        }

        public string GetOldEntityJson(T entity)
        {
            return _efRepository.GetOldEntityJson(entity);
        }

        public T GetDeletedEntity(TKey id)
        {
            return _efRepository.GetDeletedEntity(id);
        }

        public IEnumerable<T> FindDeletedEntities(Expression<Func<T, bool>> expression)
        {
            return _efRepository.FindDeletedEntities(expression);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _efRepository.Find(expression);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return _efRepository.SingleOrDefault(expression);
        }

        public T SingleOrDefaultDeleted(Expression<Func<T, bool>> expression)
        {
            return _efRepository.SingleOrDefaultDeleted(expression);
        }

        public void Create(T item)
        {
            _efRepository.Create(item);
        }

        public void Delete(T item)
        {
            _efRepository.Delete(item);
        }

        public IEnumerable<T> Find(IPipeline<T> pipline)
        {
            return _efRepository.Find(pipline);
        }

        public T Get(TKey id)
        {
            return _efRepository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _efRepository.GetAll();
        }

        public void Update(T item)
        {
            var action = LogAction.Updated.ToString();
            LogEntity(item, action);

            if (_mongoRepository.Contains(item))
            {
                _mongoRepository.Update(item);
            }

            _efRepository.Update(item);
        }

        private void LogEntity(T item, string action)
        {
            IMongoLogger logger = new MongoLogger(_mongoRepository.GetLogsCollection());
            logger.LogOperation(new Log
            {
                Date = DateTime.UtcNow,
                OldEntity = new BsonJavaScript(GetOldEntityJson(item)),
                NewEntity = new BsonJavaScript(item.ToJson()),
                EntityType = item?.GetType().BaseType.Name ?? "null",
                Action = action
            });
        }
    }
}
