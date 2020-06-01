namespace CarMarket.Services.Data.SearchServiceHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CarMarket.Data.Models;

    internal static class OrderingMutatorsProvider
    {
        public static readonly IDictionary<int, Func<IQueryable<Listing>, IQueryable<Listing>>> OrderingMutators = new Dictionary<int, Func<IQueryable<Listing>, IQueryable<Listing>>>
        {
            {
                1, listings => listings.OrderBy(l => l.CreatedOn)
            },
            {
                2, listings => listings.OrderByDescending(l => l.CreatedOn)
            },
            {
                3, listings => listings.OrderBy(l => l.Price)
            },
            {
                4, listings => listings.OrderByDescending(l => l.Price)
            },
            {
                5, listings => listings.OrderBy(l => l.ProductionYear)
            },
            {
                6, listings => listings.OrderByDescending(l => l.ProductionYear)
            },
            {
                7, listings => listings.OrderBy(l => l.Mileage)
            },
            {
                8, listings => listings.OrderByDescending(l => l.Mileage)
            },
        };
    }
}
