namespace CarMarket.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Tests.Common;
    using CarMarket.Web.ViewModels.Listings;

    using Microsoft.AspNetCore.Identity;

    using Moq;
    using Xunit;

    public class ListingsServiceTests
    {
        private const string User1Id = "1";
        private const string User1Username = "username1";
        private const string User1FirstName = "first";
        private const string User1LastName = "last";
        private const string User1Email = "asd@asd.asd";

        private const string User2Id = "2";
        private const string User2Username = "username2";

        private const string User3Id = "3";
        private const string User3Username = "username3";

        private const int NonExistentListingId = 12345;
        private const int TestListing1Id = 1;
        private const int TestListing2Id = 2;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly ListingsService sut;

        public ListingsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedData();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.Users).Returns(() => this.context.Users);

            this.sut = new ListingsService(
                new EfRepository<Listing>(this.context),
                this.mapper,
                null,
                userManagerMock.Object);
        }

        [Fact]
        public async Task GetSingleByIdAsync_ShouldReturnCorrectListing_WithGivenValidId()
        {
            this.context.Listings.Add(new Listing { SellerId = User1Id, Id = TestListing1Id });
            this.context.SaveChanges();

            var expected = new DetailsListingViewModel { Id = TestListing1Id };
            var actual = await this.sut.GetSingleByIdAsync<DetailsListingViewModel>(TestListing1Id);

            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public async Task GetSingleByIdAsync_ShouldThrowCustomNotFoundException_WithGivenInvalidId()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => this.sut.GetSingleByIdAsync<DetailsListingViewModel>(NonExistentListingId));
        }

        [Fact]
        public async Task HasPermissionToAccessAsync_ShouldReturnTrue_WhenUserHasCreatedTheListing()
        {
            this.context.Listings.Add(new Listing { SellerId = User1Id, Id = TestListing1Id });
            this.context.SaveChanges();

            var actual = await this.sut.HasPermissionToAccessAsync(User1Id, TestListing1Id);

            Assert.True(actual);
        }

        [Fact]
        public async Task HasPermissionToAccessAsync_ShouldReturnFalse_WhenUserHasNotCreatedTheListing()
        {
            this.context.Listings.Add(new Listing { SellerId = User1Id, Id = TestListing1Id });
            this.context.SaveChanges();

            var actual = await this.sut.HasPermissionToAccessAsync(User2Id, TestListing1Id);

            Assert.False(actual);
        }

        [Fact]
        public async Task GetLatestAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            var listings = await this.sut.GetLatestAsync<DetailsListingViewModel>();
            Assert.Empty(listings);
        }

        [Fact]
        public async Task GetTotalCountAsync_ShouldReturnCorrectCount()
        {
            this.context.Listings.Add(new Listing { Id = TestListing1Id });
            this.context.Listings.Add(new Listing { Id = TestListing2Id });
            this.context.SaveChanges();

            var expectedCount = this.context.Listings.Count();
            var actualCount = await this.sut.GetTotalCountAsync();

            Assert.Equal(expectedCount, actualCount);
        }

        private void SeedData()
        {
            this.context.Users.AddRange(
                    new ApplicationUser
                    {
                        Id = User1Id,
                        UserName = User1Username,
                        FirstName = User1FirstName,
                        LastName = User1LastName,
                        Email = User1Email,
                    },
                    new ApplicationUser
                    {
                        Id = User2Id,
                        UserName = User2Username,
                    },
                    new ApplicationUser
                    {
                        Id = User3Id,
                        UserName = User3Username,
                    });

            this.context.SaveChanges();
        }
    }
}
