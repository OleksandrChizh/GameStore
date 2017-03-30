using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;

namespace GameStore.Services.Dto.Utils
{
    public static class MapperToDto
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.UserId,
                BanExpiryDate = user.BanExpiryDate,
                UserName = user.UserName,                
                Type = user.NotificationType,
                Roles = user.Roles.Select(r => r.Name).ToList()
            };
        }

        public static CommentDto ToDto(this Comment comment)
        {
            int? parentCommentId = null;
            var repliedTo = string.Empty;
            var quote = string.Empty;
            if (comment.ParentComment != null)
            {
                parentCommentId = comment.ParentComment.Id;
                repliedTo = comment.ParentComment.Name;
                if (comment.IsQuote && !comment.ParentComment.Deleted)
                {
                    quote = comment.ParentComment.Body;
                }
            }

            return new CommentDto(
                comment.Id, 
                comment.Name, 
                comment.Body, 
                comment.Game.Id,
                parentCommentId,
                repliedTo,
                quote,
                comment.IsQuote);
        }

        public static GameDto ToDto(this Game game)
        {
            List<GameTranslation> translations = game.Translations.ToList();
            IDictionary<string, string> names = new Dictionary<string, string>();
            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            translations.ForEach(t =>
            {
                if (!t.Deleted)
                {
                    names.Add(t.Language, t.Name);
                    descriptions.Add(t.Language, t.Description);
                }
            });

            if (!names.ContainsKey("ru"))
            {
                names["ru"] = game.Name;
            }

            if (!descriptions.ContainsKey("ru"))
            {
                descriptions["ru"] = game.Description;
            }

            return new GameDto(
                game.Id,
                game.Key,
                names,
                descriptions,
                game.PlatformTypes.Select(pt => pt.ToDto()).ToList(),
                game.Genres.Select(g => g.ToDto()).ToList(),
                game.Price,
                game.UnitsInStock,
                game.Discounted,
                game.Deleted,
                game.ViewsCount,
                game.AddingDate,
                game.PublishingDate,
                game.ImagePath,
                game.Publishers.Select(g => g.ToDto()).ToList());
        }

        public static GenreDto ToDto(this Genre genre)
        {
            int? parentGenreId = null;
            if (genre.ParentGenre != null)
            {
                parentGenreId = genre.ParentGenre.Id;
            }

            return new GenreDto(
                genre.Id, 
                genre.Name,
                parentGenreId);
        }

        public static PlatformTypeDto ToDto(this PlatformType platformType)
        {
            return new PlatformTypeDto(
                platformType.Id, 
                platformType.Type);
        }

        public static PublisherDto ToDto(this Publisher publisher)
        {
            List<PublisherTranslation> translations = publisher.Translations.ToList();
            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            translations.ForEach(t => descriptions.Add(t.Language, t.Description));

            if (!descriptions.ContainsKey("ru"))
            {
                descriptions["ru"] = publisher.Description;
            }

            return new PublisherDto(
                publisher.Id,
                publisher.CompanyName,
                descriptions,
                publisher.HomePage);
        }

        public static OrderDetailsDto ToDto(this OrderDetails orderDetails)
        {
            return new OrderDetailsDto(
                orderDetails.Id,
                orderDetails.ProductId,
                orderDetails.Price,
                orderDetails.Quantity,
                orderDetails.Discount);
        }

        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto(
                order.Id,
                order.CustomerId,
                order.OrderDate,
                order.PayingDate,
                order.ShippedDate,
                order.OrderDetails.Select(o => o.ToDto()).ToList());
        }
    }
}
