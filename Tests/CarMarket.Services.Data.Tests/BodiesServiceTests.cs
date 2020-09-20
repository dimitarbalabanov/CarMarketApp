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
        private const int TestType1TestId = 1;
        private const int TestType2TestId = 2;
        private const int TestType3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly BodiesService sut;

        public BodiesServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedBodies();
            this.sut = new BodiesService(new EfRepository<Body>(this.context), this.mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<BodySelectListViewModel>
            {
                new BodySelectListViewModel { Type = TestType1 },
                new BodySelectListViewModel { Type = TestType2 },
                new BodySelectListViewModel { Type = TestType3 },
            };
            var actual = (await this.sut.GetAllAsync<BodySelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Type, actual[i].Type);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Bodies.RemoveRange(this.context.Bodies);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<BodySelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestType1TestId, TestType1)]
        [InlineData(TestType2TestId, TestType2)]
        [InlineData(TestType3TestId, TestType3)]
        public async Task GetBodyTypeByIdAsync_ShouldReturnCorrectType_WhenBodyExists(int id, string expected)
        {
            var actual = await this.sut.GetBodyTypeByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetBodyTypeByIdAsync_ShouldReturnNull_WhenBodyDoesNotExist()
        {
            var actual = await this.sut.GetBodyTypeByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(TestType1TestId)]
        [InlineData(TestType2TestId)]
        [InlineData(TestType3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenBodyExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenBodyDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedBodies()
        {
            this.context.Bodies.AddRange(
                            new Body { Id = TestType1TestId, Type = TestType1 },
                            new Body { Id = TestType2TestId, Type = TestType2 },
                            new Body { Id = TestType3TestId, Type = TestType3 });
            this.context.SaveChanges();
        }
    }
}
