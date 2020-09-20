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
    using CarMarket.Web.ViewModels.Fuels;

    using Xunit;

    public class FuelsServiceTests
    {
        private const string TestType1 = "Electric";
        private const string TestType2 = "Petrol";
        private const string TestType3 = "Diesel";
        private const int TestId = 1;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public FuelsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            await this.SeedMultiple();
            var service = this.CreateRepositoryAndService();

            var expected = new List<FuelSelectListViewModel>
            {
                new FuelSelectListViewModel { Type = TestType1 },
                new FuelSelectListViewModel { Type = TestType2 },
                new FuelSelectListViewModel { Type = TestType3 },
            };

            var actual = (await service.GetAllAsync<FuelSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Type, actual[i].Type);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetAllAsync<FuelSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetFuelTypeByIdAsync_ShouldReturnCorrectType_WhenFuelExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetFuelTypeByIdAsync(TestId);

            Assert.Equal(TestType1, actual);
        }

        [Fact]
        public async Task GetFuelTypeByIdAsync_ShouldReturnNull_WhenFuelDoesNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetFuelTypeByIdAsync(TestId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenFuelExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenFuelDoesNotExists()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.False(actual);
        }

        private FuelsService CreateRepositoryAndService()
        {
            var repository = new EfRepository<Fuel>(this.context);
            var service = new FuelsService(repository, this.mapper);
            return service;
        }

        private async Task SeedMultiple()
        {
            await this.context.Fuels.AddRangeAsync(
               new Fuel { Type = TestType1 },
               new Fuel { Type = TestType2 },
               new Fuel { Type = TestType3 });
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSingle()
        {
            await this.context.Fuels.AddAsync(new Fuel { Id = TestId, Type = TestType1 });
            await this.context.SaveChangesAsync();
        }
    }
}
