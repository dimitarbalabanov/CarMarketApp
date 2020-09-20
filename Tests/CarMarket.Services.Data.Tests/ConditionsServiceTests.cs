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
    using CarMarket.Web.ViewModels.Conditions;

    using Xunit;

    public class ConditionsServiceTests
    {
        private const string TestType1 = "New";
        private const string TestType2 = "Used";
        private const string TestType3 = "For parts";
        private const int TestId = 1;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public ConditionsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            await this.SeedMultiple();
            var service = this.CreateRepositoryAndService();

            var expected = new List<ConditionSelectListViewModel>
            {
                new ConditionSelectListViewModel { Type = TestType1 },
                new ConditionSelectListViewModel { Type = TestType2 },
                new ConditionSelectListViewModel { Type = TestType3 },
            };

            var actual = (await service.GetAllAsync<ConditionSelectListViewModel>()).ToList();

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
            var actual = await service.GetAllAsync<ConditionSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetConditionTypeByIdAsync_ShouldReturnCorrectType_WhenConditionExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetConditionTypeByIdAsync(TestId);

            Assert.Equal(TestType1, actual);
        }

        [Fact]
        public async Task GetConditionTypeByIdAsync_ShouldReturnNull_WhenConditionDoesNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetConditionTypeByIdAsync(TestId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenConditionExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenConditionDoesNotExists()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.False(actual);
        }

        private ConditionsService CreateRepositoryAndService()
        {
            var repository = new EfRepository<Condition>(this.context);
            var service = new ConditionsService(repository, this.mapper);
            return service;
        }

        private async Task SeedMultiple()
        {
            await this.context.Conditions.AddRangeAsync(
                 new Condition { Type = TestType1 },
                 new Condition { Type = TestType2 },
                 new Condition { Type = TestType3 });
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSingle()
        {
            await this.context.Conditions.AddAsync(new Condition { Id = TestId, Type = TestType1 });
            await this.context.SaveChangesAsync();
        }
    }
}
