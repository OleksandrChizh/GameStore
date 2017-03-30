using System;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces.Enums;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByPublishingDateFilter : IFilter<Game>
    {
        private readonly DateTime _from;

        public GamesByPublishingDateFilter(PublishingDatePeriod period)
        {
            switch (period)
            {
                case PublishingDatePeriod.LastWeek:
                    _from = DateTime.UtcNow.AddDays(-7);
                    break;
                case PublishingDatePeriod.LastMonth:
                    _from = DateTime.UtcNow.AddMonths(-1);
                    break;
                case PublishingDatePeriod.LastYear:
                    _from = DateTime.UtcNow.AddYears(-1);
                    break;
                case PublishingDatePeriod.TwoYears:
                    _from = DateTime.UtcNow.AddYears(-2);
                    break;
                case PublishingDatePeriod.ThreeYears:
                    _from = DateTime.UtcNow.AddYears(-3);
                    break;

                default:
                    _from = DateTime.MinValue;
                    break;
            }
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.Where(g => g.PublishingDate >= _from && g.PublishingDate < DateTime.UtcNow);
        }
    }
}
