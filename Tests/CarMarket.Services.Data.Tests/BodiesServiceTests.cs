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
    using CarMarket.Web.ViewModels.Bodies;

    using Xunit;

    public class BodiesServiceTests
    {
        private const string TestType1 = "Convertible";
        private const string TestType2 = "Coupe";
        private const string TestType3 = "Hatchback";
        private const int TestId = 1;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public BodiesServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            await this.SeedMultiple();
            var service = this.CreateRepositoryAndService();

            var expected = new List<BodySelectListViewModel>
            {
                new BodySelectListViewModel { Type = TestType1 },
                new BodySelectListViewModel { Type = TestType2 },
                new BodySelectListViewModel { Type = TestType3 },
            };

            var actual = (await service.GetAllAsync<BodySelectListViewModel>()).ToList();

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
            var actual = await service.GetAllAsync<BodySelectListViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetBodyTypeByIdAsync_ShouldReturnCorrectType_WhenBodyExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetBodyTypeByIdAsync(TestId);

            Assert.Equal(TestType1, actual);
        }

        [Fact]
        public async Task GetBodyTypeByIdAsync_ShouldReturnNull_WhenBodyDoesNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetBodyTypeByIdAsync(TestId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenBodyExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenBodyDoesNotExists()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.False(actual);
        }

        private BodiesService CreateRepositoryAndService()
        {
            var repository = new EfRepository<Body>(this.context);
            var service = new BodiesService(repository, this.mapper);
            return service;
        }

        private async Task SeedMultiple()
        {
            await this.context.Bodies.AddRangeAsync(
                            new Body { Type = TestType1 },
                            new Body { Type = TestType2 },
                            new Body { Type = TestType3 });
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSingle()
        {
            await this.context.Bodies.AddAsync(new Body { Id = TestId, Type = TestType1 });
            await this.context.SaveChangesAsync();
        }
    }
}
