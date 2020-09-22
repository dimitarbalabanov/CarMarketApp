namespace CarMarket.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Tests.Common;
    using CarMarket.Web.ViewModels.Administration.Users;

    using Microsoft.AspNetCore.Identity;

    using Moq;
    using Xunit;

    public class UsersServiceTests
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

        private const string NonExistentUserId = "1234567";
        private const int TotalUsersCount = 3;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly UsersService sut;

        public UsersServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedUsers();
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.Users).Returns(() => this.context.Users);
            this.sut = new UsersService(userManagerMock.Object, this.mapper);
        }

        [Fact]
        public async Task GetTotalCountAsync_ShouldReturnCorrectCount()
        {
            var count = await this.sut.GetTotalCountAsync();
            Assert.Equal(TotalUsersCount, count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<UserViewModel>
            {
                new UserViewModel { Id = User1Id, UserName = User1Username },
                new UserViewModel { Id = User2Id, UserName = User2Username },
                new UserViewModel { Id = User3Id, UserName = User3Username },
            };
            var actual = (await this.sut.GetAllAsync<UserViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].UserName, actual[i].UserName);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Users.RemoveRange(this.context.Users);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<UserViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetUserInfoByIdAsync_ShouldReturnCorrectInfo_WhenUserWithGivenIdExists()
        {
            var actual = await this.sut.GetUserInfoByIdAsync<UserDetailsViewModel>(User1Id);

            Assert.Equal(User1Username, actual.UserName);
            Assert.Equal(User1Email, actual.Email);
            Assert.Equal(User1FirstName, actual.FirstName);
            Assert.Equal(User1LastName, actual.LastName);
        }

        [Fact]
        public async Task GetUserInfoByIdAsync_ShouldThrowCustomNotFoundException_WhenUserWithGivenIdDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => this.sut.GetUserInfoByIdAsync<UserDetailsViewModel>(NonExistentUserId));
        }

        private void SeedUsers()
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
