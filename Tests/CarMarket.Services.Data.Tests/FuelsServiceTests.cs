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
        private const int TestType1TestId = 1;
        private const int TestType2TestId = 2;
        private const int TestType3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly FuelsService sut;

        public FuelsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedFuels();
            this.sut = new FuelsService(new EfRepository<Fuel>(this.context), this.mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<FuelSelectListViewModel>
            {
                new FuelSelectListViewModel { Type = TestType1 },
                new FuelSelectListViewModel { Type = TestType2 },
                new FuelSelectListViewModel { Type = TestType3 },
            };

            var actual = (await this.sut.GetAllAsync<FuelSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Type, actual[i].Type);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Fuels.RemoveRange(this.context.Fuels);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<FuelSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestType1TestId, TestType1)]
        [InlineData(TestType2TestId, TestType2)]
        [InlineData(TestType3TestId, TestType3)]
        public async Task GetFuelTypeByIdAsync_ShouldReturnCorrectType_WhenFuelExists(int id, string expected)
        {
            var actual = await this.sut.GetFuelTypeByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetFuelTypeByIdAsync_ShouldReturnNull_WhenFuelDoesNotExist()
        {
            var actual = await this.sut.GetFuelTypeByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(TestType1TestId)]
        [InlineData(TestType2TestId)]
        [InlineData(TestType3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenFuelExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenFuelDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedFuels()
        {
            this.context.Fuels.AddRange(
               new Fuel { Type = TestType1 },
               new Fuel { Type = TestType2 },
               new Fuel { Type = TestType3 });
            this.context.SaveChanges();
        }
    }
}
