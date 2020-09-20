namespace CarMarket.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Services.Data.Tests.Common;
    using CarMarket.Web.ViewModels.Colors;

    using Xunit;

    public class ColorsServiceTests
    {
        private const string TestName1 = "White";
        private const string TestName2 = "Silver";
        private const string TestName3 = "Black";
        private const int TestId = 1;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ColorsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            await this.SeedMultiple();
            var service = this.CreateRepositoryAndService();

            var expected = new List<ColorSelectListViewModel>
            {
                new ColorSelectListViewModel { Name = TestName1 },
                new ColorSelectListViewModel { Name = TestName2 },
                new ColorSelectListViewModel { Name = TestName3 },
            };

            var actual = (await service.GetAllAsync<ColorSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetAllAsync<ColorSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetColorNameByIdAsync_ShouldReturnCorrectName_WhenColorExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetColorNameByIdAsync(TestId);

            Assert.Equal(TestName1, actual);
        }

        [Fact]
        public async Task GetColorNameByIdAsync_ShouldReturnNull_WhenColorDoesNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetColorNameByIdAsync(TestId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenColorExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenColorDoesNotExists()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.False(actual);
        }

        private ColorsService CreateRepositoryAndService()
        {
            var repository = new EfRepository<Color>(this.context);
            var service = new ColorsService(repository, this.mapper);
            return service;
        }

        private async Task SeedMultiple()
        {
            await this.context.Colors.AddRangeAsync(
                new Color { Name = TestName1 },
                new Color { Name = TestName2 },
                new Color { Name = TestName3 });
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSingle()
        {
            await this.context.Colors.AddAsync(new Color { Id = TestId, Name = TestName1 });
            await this.context.SaveChangesAsync();
        }
    }
}
