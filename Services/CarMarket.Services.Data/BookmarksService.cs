using CarMarket.Data.Common.Repositories;
using CarMarket.Data.Models;
using CarMarket.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Services.Data
{
    public class BookmarksService : IBookmarksService
    {
        private readonly IRepository<ApplicationUserBookmarkListing> bookmarksRepository;

        public BookmarksService(IRepository<ApplicationUserBookmarkListing> bookmarksRepository)
        {
            this.bookmarksRepository = bookmarksRepository;
        }

        public async Task AddAsync(string userId, int listingId)
        {
            var bookmark = new ApplicationUserBookmarkListing
            {
                UserId = userId,
                ListingId = listingId,
            };

            await this.bookmarksRepository.AddAsync(bookmark);
            await this.bookmarksRepository.SaveChangesAsync();
        }

        public async Task<bool> IsBookmarkedAsync(string userId, int listingId)
        {
            var bookmark = await this.bookmarksRepository
                .All()
                .FirstOrDefaultAsync(x => x.ListingId == listingId && x.UserId == userId);

            return bookmark != null;
        }

        public async Task RemoveAsync(string userId, int listingId)
        {
            var bookmark = await this.bookmarksRepository
                .All()
                .FirstOrDefaultAsync(x => x.ListingId == listingId && x.UserId == userId);

            this.bookmarksRepository.Delete(bookmark);
            await this.bookmarksRepository.SaveChangesAsync();
        }
    }
}
