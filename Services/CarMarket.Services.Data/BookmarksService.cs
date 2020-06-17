namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class BookmarksService : IBookmarksService
    {
        private readonly IRepository<ApplicationUserBookmarkListing> bookmarksRepository;
        private readonly IMapper mapper;
        private readonly IRepository<Listing> listingsRepository;

        public BookmarksService(IRepository<ApplicationUserBookmarkListing> bookmarksRepository, IMapper mapper, IRepository<Listing> listingsRepository)
        {
            this.bookmarksRepository = bookmarksRepository;
            this.mapper = mapper;
            this.listingsRepository = listingsRepository;
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

        public async Task<IEnumerable<T>> GetAllListingsByUserIdAsync<T>(string userId)
        {
            var bookmarkedListingsIds = await this.bookmarksRepository
                .AllAsNoTracking()
                .Where(b => b.UserId == userId)
                .Select(b => b.ListingId)
                .ToListAsync();

            var bookmarkedListingsQuery = this.listingsRepository
                .AllAsNoTracking()
                .Where(x => bookmarkedListingsIds.Contains(x.Id))
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images)
                .OrderByDescending(l => l.CreatedOn);

            var bookmarkedListings = await this.mapper.ProjectTo<T>(bookmarkedListingsQuery).ToListAsync();
            return bookmarkedListings;
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
