﻿namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookmarksService
    {
        Task AddAsync(string userId, int listingId);

        Task RemoveAsync(string userId, int listingId);

        Task<bool> IsBookmarkedAsync(string userId, int listingId);

        Task<IEnumerable<T>> GetAllListingsByUserIdAsync<T>(string userId);
    }
}
