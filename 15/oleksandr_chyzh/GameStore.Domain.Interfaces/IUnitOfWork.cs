using System;
using GameStore.Domain.Core.Models;

namespace GameStore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Comment, int> Comments { get; }

        IRepository<Game, int> Games { get; }

        IRepository<Genre, int> Genres { get; }

        IRepository<PlatformType, int> PlatformTypes { get; }

        IRepository<Publisher, int> Publishers { get; }

        IRepository<OrderDetails, int> OrderDetails { get; }

        IRepository<Order, int> Orders { get; }    
        
        IRepository<GameTranslation, int> GameTranslations { get; }
        
        IRepository<PublisherTranslation, int> PublisherTranslations { get; }   

        IRepository<User, string> Users { get; }

        IRepository<Role, int> Roles { get; }

        void Save();
    }
}
