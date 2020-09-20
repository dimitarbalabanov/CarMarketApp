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
        private const int TestType1TestId = 1;
        private const int TestType2TestId = 2;
        private const int TestType3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly ConditionsService sut;

        public ConditionsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedConditions();
            this.sut = new ConditionsService(new EfRepository<Condition>(this.context), this.mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<ConditionSelectListViewModel>
            {
                new ConditionSelectListViewModel { Type = TestType1 },
                new ConditionSelectListViewModel { Type = TestType2 },
                new ConditionSelectListViewModel { Type = TestType3 },
            };
            var actual = (await this.sut.GetAllAsync<ConditionSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Type, actual[i].Type);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Conditions.RemoveRange(this.context.Conditions);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<ConditionSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestType1TestId, TestType1)]
        [InlineData(TestType2TestId, TestType2)]
        [InlineData(TestType3TestId, TestType3)]
        public async Task GetConditionTypeByIdAsync_ShouldReturnCorrectType_WhenConditionExists(int id, string expected)
        {
            var actual = await this.sut.GetConditionTypeByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetConditionTypeByIdAsync_ShouldReturnNull_WhenConditionDoesNotExist()
        {
            var actual = await this.sut.GetConditionTypeByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(TestType1TestId)]
        [InlineData(TestType2TestId)]
        [InlineData(TestType3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenConditionExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenConditionDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedConditions()
        {
            this.context.Conditions.AddRange(
                 new Condition { Type = TestType1 },
                 new Condition { Type = TestType2 },
                 new Condition { Type = TestType3 });
            this.context.SaveChanges();
        }
    }
}
