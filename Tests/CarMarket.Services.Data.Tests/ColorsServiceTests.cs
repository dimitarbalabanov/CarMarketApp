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
        private const int TestName1TestId = 1;
        private const int TestName2TestId = 2;
        private const int TestName3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly ColorsService sut;

        public ColorsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedColors();
            this.sut = new ColorsService(new EfRepository<Color>(this.context), this.mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<ColorSelectListViewModel>
            {
                new ColorSelectListViewModel { Name = TestName1 },
                new ColorSelectListViewModel { Name = TestName2 },
                new ColorSelectListViewModel { Name = TestName3 },
            };
            var actual = (await this.sut.GetAllAsync<ColorSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Colors.RemoveRange(this.context.Colors);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<ColorSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestName1TestId, TestName1)]
        [InlineData(TestName2TestId, TestName2)]
        [InlineData(TestName3TestId, TestName3)]
        public async Task GetColorNameByIdAsync_ShouldReturnCorrectName_WhenColorExists(int id, string expected)
        {
            var actual = await this.sut.GetColorNameByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetColorNameByIdAsync_ShouldReturnNull_WhenColorDoesNotExist()
        {
            var actual = await this.sut.GetColorNameByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(TestName1TestId)]
        [InlineData(TestName2TestId)]
        [InlineData(TestName3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenColorExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenColorDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedColors()
        {
            this.context.Colors.AddRange(
                new Color { Id = TestName1TestId, Name = TestName1 },
                new Color { Id = TestName2TestId, Name = TestName2 },
                new Color { Id = TestName3TestId, Name = TestName3 });
            this.context.SaveChanges();
        }
    }
}
