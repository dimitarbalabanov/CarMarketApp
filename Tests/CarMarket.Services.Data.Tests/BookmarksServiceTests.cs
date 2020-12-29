namespace CarMarket.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Services.Data.Tests.Common;
    using CarMarket.Web.ViewModels.Listings;
    using Xunit;

    public class BookmarksServiceTests
    {
        private const string TestUserId = "1";
        private const int TestListing1Id = 1;
        private const int TestListing2Id = 2;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly BookmarksService sut;

        public BookmarksServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedData();
            this.sut = new BookmarksService(
                new EfRepository<ApplicationUserBookmarkListing>(this.context),
                this.mapper,
                new EfRepository<Listing>(this.context));
        }

        [Fact]
        public async Task AddAsync_ShouldCorrectlyCreateABookmark()
        {
            await this.sut.AddAsync(TestUserId, TestListing1Id);

            var expectedCount = 1;
            var actualCount = this.context.ApplicationUsersBookmarkListings.Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task GetAllListingsByUserIdAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            var actualBookmarks = await this.sut.GetAllListingsByUserIdAsync<BookmarksListingViewModel>(TestUserId);
            Assert.Empty(actualBookmarks);
        }

        [Fact]
        public async Task IsBookmarkedAsync_ShouldReturnTrue_WhenUserHasBookmarkedTheListing()
        {
            this.context.ApplicationUsersBookmarkListings.Add(new ApplicationUserBookmarkListing { UserId = TestUserId, ListingId = TestListing1Id });
            this.context.SaveChanges();

            var actual = await this.sut.IsBookmarkedAsync(TestUserId, TestListing1Id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsBookmarkedAsync_ShouldReturnFalse_WhenUserHasNotBookmarkedTheListing()
        {
            var actual = await this.sut.IsBookmarkedAsync(TestUserId, TestListing1Id);

            Assert.False(actual);
        }

        [Fact]
        public async Task RemoveAsync_ShouldCorrectlyRemoveABookmark()
        {
            this.context.ApplicationUsersBookmarkListings.Add(new ApplicationUserBookmarkListing { UserId = TestUserId, ListingId = TestListing1Id });
            this.context.SaveChanges();

            await this.sut.RemoveAsync(TestUserId, TestListing1Id);

            var expectedCount = 0;
            var actualBookmarksCount = this.context.ApplicationUsersBookmarkListings.Where(b => b.UserId == TestUserId).ToList().Count;

            Assert.Equal(expectedCount, actualBookmarksCount);
        }

        private void SeedData()
        {
            this.context.Users.Add(new ApplicationUser { Id = TestUserId });
            this.context.Listings.Add(new Listing { Id = TestListing1Id });
            this.context.Listings.Add(new Listing { Id = TestListing2Id });
            this.context.SaveChanges();
        }
    }
}
