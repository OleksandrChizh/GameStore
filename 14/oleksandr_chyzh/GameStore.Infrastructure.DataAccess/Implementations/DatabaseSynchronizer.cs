using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Infrastructure.EFDataAccess;
using GameStore.Infrastructure.MongoDataAccess;
using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameStore.Infrastructure.DataAccess.Implementations
{
    public class DatabaseSynchronizer : IDatabaseSynchronizer
    {
        private readonly ApplicationContext _applicationContext = new ApplicationContext();
        private readonly MongoContext _mongoContext = new MongoContext();

        public void Synchronize()
        {
            SynchronizePublishers();
            SynchronizeGenres();
            SynchronizeGames();
            SynchronizeOrderWithDetails();
        }

        private void SynchronizeGames()
        {
            foreach (Product product in _mongoContext.Products)
            {
                Game efGame = _applicationContext.Games.SingleOrDefault(g => g.Key == product.Id);
                if (efGame == null)
                {
                    AddGameInEfDb(product);
                }
                else
                {
                    UpdateGameFromEfDb(product, efGame);
                }
            }

            _applicationContext.SaveChanges();

            foreach (Game game in _applicationContext.Games)
            {
                CheckGameDeletingFormMongoDb(game);
            }
        }

        private void CheckGameDeletingFormMongoDb(Game game)
        {
            const string objectIdRegex = @"[0-9a-f]{24}";
            if (Regex.IsMatch(game.Key, objectIdRegex))
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("_id", new ObjectId(game.Key));
                List<Product> result = _mongoContext.ProductsCollection.FindSync(filter).ToList();
                if (result.FirstOrDefault() == null)
                {
                    game.Deleted = true;
                    _applicationContext.Entry(game).State = EntityState.Modified;
                    _applicationContext.SaveChanges();
                }
            }
        }

        private void AddGameInEfDb(Product product)
        {
            Genre genre = _mongoContext.Categories.SingleOrDefault(c => c.Id == product.CategoryId);
            Publisher publisher = _mongoContext.Suppliers.SingleOrDefault(c => c.Id == product.SupplierId);

            var genres = genre == null ? new List<Genre>() : _applicationContext.Genres.Where(g => g.Name == genre.Name).ToList();
            var publishers = publisher == null ? new List<Publisher>() : _applicationContext.Publishers.Where(p => p.CompanyName == publisher.CompanyName).ToList();

            _applicationContext.Games.Add(new Game
            {
                Key = product.Id,
                Name = product.ProductName,
                Description = "Undefined",
                Price = product.UnitPrice,
                UnitsInStock = (short)product.UnitsInStock,
                Discounted = product.Discontinued != 0,
                AddingDate = DateTime.UtcNow,
                PublishingDate = new DateTime(),
                Comments = new List<Comment>(),
                PlatformTypes = new List<PlatformType>(),
                Genres = genres,
                Publishers = publishers
            });
        }

        private void UpdateGameFromEfDb(Product product, Game efGame)
        {
            efGame.Name = product.ProductName;
            efGame.Price = product.UnitPrice;
            efGame.UnitsInStock = (short)product.UnitsInStock;
            efGame.Discounted = product.Discontinued != 0;

            _applicationContext.Entry(efGame).State = EntityState.Modified;
        }

        private void SynchronizeGenres()
        {
            foreach (Genre genre in _mongoContext.Categories)
            {
                Genre efGenre = _applicationContext.Genres.SingleOrDefault(p => p.Name == genre.Name);
                if (efGenre == null)
                {
                    _applicationContext.Genres.Add(genre);
                }
            }

            _applicationContext.SaveChanges();
        }

        private void SynchronizePublishers()
        {
            foreach (Publisher publisher in _mongoContext.Suppliers)
            {
                Publisher efPublisher = _applicationContext.Publishers.SingleOrDefault(p => p.CompanyName == publisher.CompanyName);
                if (efPublisher == null)
                {
                    _applicationContext.Publishers.Add(publisher);
                }
                else
                {
                    efPublisher.HomePage = publisher.HomePage;

                    _applicationContext.Entry(efPublisher).State = EntityState.Modified;
                }
            }

            _applicationContext.SaveChanges();
        }

        private void SynchronizeOrderWithDetails()
        {
            foreach (MongoOrder order in _mongoContext.Orders)
            {
                if (!order.OutDated)
                {
                    FilterDefinition<MongoOrder> findOrderFilter = Builders<MongoOrder>.Filter.Eq("_id", new ObjectId(order.Id));
                    UpdateDefinition<MongoOrder> updateOrder = Builders<MongoOrder>.Update.Set("OutDated", true);
                    _mongoContext.OrderCollection.UpdateOne(findOrderFilter, updateOrder);

                    List<MongoOrderDetails> mongoOrderDetails = _mongoContext.OrdersDetails.Where(od => od.OrderId == order.OrderId).ToList();
                    List<OrderDetails> orderDetails = mongoOrderDetails.Select(od => new OrderDetails
                    {
                        ProductId = GetEfGameIdFromMongoProductId(od.ProductId),
                        Price = od.UnitPrice,
                        Quantity = od.Quantity,
                        Discount = (float)od.Discount
                    }).ToList();

                    _applicationContext.OrderDetails.AddRange(orderDetails);
                    _applicationContext.Orders.Add(new Order
                    {
                        PayingDate = DateTime.Parse(order.OrderDate),
                        OrderDate = DateTime.Parse(order.OrderDate),
                        OrderDetails = orderDetails,
                        CustomerId = order.CustomerId
                    });
                }
            }

            _applicationContext.SaveChanges();
        }

        private int GetEfGameIdFromMongoProductId(int productId)
        {
            Product product = _mongoContext.Products.Single(p => p.ProductId == productId);
            Game efGame = _applicationContext.Games.Single(g => g.Key == product.Id);

            return efGame.Id;
        }
    }   
}
