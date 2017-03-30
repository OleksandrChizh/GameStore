using System;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Implementations.Repositories;
using GameStore.Infrastructure.EFDataAccess;
using GameStore.Infrastructure.MongoDataAccess;

namespace GameStore.Infrastructure.DataAccess.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _efDbContext;
        private readonly Lazy<RepositoryDecorator<Comment, int>> _comments;
        private readonly Lazy<RepositoryDecorator<PlatformType, int>> _platformTypes;
        private readonly Lazy<RepositoryDecorator<OrderDetails, int>> _orderDetails;
        private readonly Lazy<RepositoryDecorator<Order, int>> _orders;
        private readonly Lazy<RepositoryDecorator<Genre, int>> _genres;
        private readonly Lazy<RepositoryDecorator<Publisher, int>> _publishers;
        private readonly Lazy<RepositoryDecorator<Game, int>> _games;
        private readonly Lazy<RepositoryDecorator<GameTranslation, int>> _gameTranslations;
        private readonly Lazy<RepositoryDecorator<PublisherTranslation, int>> _publisherTranslations;
        private readonly Lazy<RepositoryDecorator<User, string>> _users;
        private readonly Lazy<RepositoryDecorator<Role, int>> _roles;
        private bool _disposed;

        public UnitOfWork()
        {
            _efDbContext = new ApplicationContext();
            var mongoContext = new MongoContext();

            _comments = new Lazy<RepositoryDecorator<Comment, int>>(
                () => new RepositoryDecorator<Comment, int>(
                    new GenericRepository<Comment, int>(_efDbContext), 
                    new MongoRepository<Comment>(mongoContext)));

            _platformTypes = new Lazy<RepositoryDecorator<PlatformType, int>>(
                () => new RepositoryDecorator<PlatformType, int>(
                    new PlatfornTypeRepository(_efDbContext), 
                    new MongoRepository<PlatformType>(mongoContext)));

            _orderDetails = new Lazy<RepositoryDecorator<OrderDetails, int>>(
                () => new RepositoryDecorator<OrderDetails, int>(
                    new GenericRepository<OrderDetails, int>(_efDbContext), 
                    new MongoRepository<OrderDetails>(mongoContext)));

            _orders = new Lazy<RepositoryDecorator<Order, int>>(
                () => new RepositoryDecorator<Order, int>(
                    new GenericRepository<Order, int>(_efDbContext),
                    new MongoRepository<Order>(mongoContext)));

            _genres = new Lazy<RepositoryDecorator<Genre, int>>(
                () => new RepositoryDecorator<Genre, int>(
                    new GenreRepository(_efDbContext),
                    new MongoRepository<Genre>(mongoContext)));

            _publishers = new Lazy<RepositoryDecorator<Publisher, int>>(
                () => new RepositoryDecorator<Publisher, int>(
                    new PublisherRepository(_efDbContext), 
                    new MongoRepository<Publisher>(mongoContext)));

            _games = new Lazy<RepositoryDecorator<Game, int>>(
                () => new RepositoryDecorator<Game, int>(
                    new GameRepository(_efDbContext), 
                    new MongoRepository<Game>(mongoContext)));

            _gameTranslations = new Lazy<RepositoryDecorator<GameTranslation, int>>(
                () => new RepositoryDecorator<GameTranslation, int>(
                    new GenericRepository<GameTranslation, int>(_efDbContext), 
                    new MongoRepository<GameTranslation>(mongoContext)));

            _publisherTranslations = new Lazy<RepositoryDecorator<PublisherTranslation, int>>(
                () => new RepositoryDecorator<PublisherTranslation, int>(
                    new GenericRepository<PublisherTranslation, int>(_efDbContext),
                    new MongoRepository<PublisherTranslation>(mongoContext)));

            _users = new Lazy<RepositoryDecorator<User, string>>(
                () => new RepositoryDecorator<User, string>(
                    new GenericRepository<User, string>(_efDbContext), 
                    new MongoRepository<User>(mongoContext)));

            _roles = new Lazy<RepositoryDecorator<Role, int>>(
                () => new RepositoryDecorator<Role, int>(
                    new GenericRepository<Role, int>(_efDbContext),
                    new MongoRepository<Role>(mongoContext)));
        }

        public IRepository<Comment, int> Comments => _comments.Value;

        public IRepository<Game, int> Games => _games.Value;

        public IRepository<Genre, int> Genres => _genres.Value;

        public IRepository<PlatformType, int> PlatformTypes => _platformTypes.Value;

        public IRepository<Publisher, int> Publishers => _publishers.Value;

        public IRepository<OrderDetails, int> OrderDetails => _orderDetails.Value;

        public IRepository<Order, int> Orders => _orders.Value;

        public IRepository<GameTranslation, int> GameTranslations => _gameTranslations.Value;

        public IRepository<PublisherTranslation, int> PublisherTranslations => _publisherTranslations.Value;

        public IRepository<User, string> Users => _users.Value;

        public IRepository<Role, int> Roles => _roles.Value;

        public void Save()
        {
            _efDbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _efDbContext.Dispose();
            }

            _disposed = true;
        }
    }
}
