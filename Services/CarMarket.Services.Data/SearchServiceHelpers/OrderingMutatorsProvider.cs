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
                0, listings => listings.OrderByDescending(l => l.CreatedOn)
            },
            {
                1, listings => listings.OrderBy(l => l.CreatedOn)
            },
            {
                2, listings => listings.OrderBy(l => l.Price)
            },
            {
                3, listings => listings.OrderByDescending(l => l.Price)
            },
            {
                4, listings => listings.OrderBy(l => l.ProductionYear)
            },
            {
                5, listings => listings.OrderByDescending(l => l.ProductionYear)
            },
            {
                6, listings => listings.OrderBy(l => l.Mileage)
            },
            {
                7, listings => listings.OrderByDescending(l => l.Mileage)
            },
        };
    }
}
