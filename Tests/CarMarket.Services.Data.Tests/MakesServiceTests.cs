namespace CarMarket.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Tests.Common;
    using CarMarket.Web.ViewModels.Administration.Makes;
    using CarMarket.Web.ViewModels.Makes;

    using Xunit;

    public class MakesServiceTests
    {
        private const string TestName1 = "Mercedes";
        private const string TestName2 = "Audi";
        private const string TestName3 = "BMW";
        private const string NonExistentName = "NoName";
        private const int TestName1TestId = 1;
        private const int TestName2TestId = 2;
        private const int TestName3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly MakesService sut;

        public MakesServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedMakes();
            this.sut = new MakesService(new EfRepository<Make>(this.context), this.mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldCorrectlyCreateAMake()
        {
            var input = new CreateMakeInputModel { Name = TestName1 };
            var makeId = await this.sut.CreateAsync<CreateMakeInputModel>(input);
            var createdMake = this.context.Makes.FirstOrDefault(m => m.Id == makeId);

            Assert.Equal(input.Name, createdMake.Name);
        }

        [Theory]
        [InlineData(TestName1)]
        [InlineData(TestName2)]
        [InlineData(TestName3)]
        public async Task ExistsByNameAsync_ShouldReturnTrue_WhenMakeWithGivenNameExists(string name)
        {
            var actual = await this.sut.ExistsByNameAsync(name);

            Assert.True(actual);
        }

        [Fact]
        public async Task ExistsByNameAsync_ShouldReturnFalse_WhenMakeWithGivenNameDoesNotExist()
        {
            var actual = await this.sut.ExistsByNameAsync(NonExistentName);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(TestName1TestId, TestName1)]
        [InlineData(TestName2TestId, TestName2)]
        [InlineData(TestName3TestId, TestName3)]
        public async Task GetMakeNameByIdAsync_ShouldReturnCorrectName_WhenMakeExists(int id, string expected)
        {
            var actual = await this.sut.GetMakeNameByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetMakeNameByIdAsync_ShouldReturnNull_WhenMakeDoesNotExist()
        {
            var actual = await this.sut.GetMakeNameByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<MakeSelectListViewModel>
            {
                new MakeSelectListViewModel { Name = TestName1 },
                new MakeSelectListViewModel { Name = TestName2 },
                new MakeSelectListViewModel { Name = TestName3 },
            };
            var actual = (await this.sut.GetAllAsync<MakeSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Makes.RemoveRange(this.context.Makes);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<MakeSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestName1TestId, TestName1)]
        [InlineData(TestName2TestId, TestName2)]
        [InlineData(TestName3TestId, TestName3)]
        public async Task GetSingleByIdAsync_ShouldReturnCorrectMake_WhenMakeExist(int id, string name)
        {
            var expected = new MakeViewModel { Id = id, Name = name };
            var actual = await this.sut.GetSingleByIdAsync<MakeViewModel>(id);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public async Task GetSingleByIdAsync_ShouldThrowCustomNotFoundException_WhenMakeDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => this.sut.GetSingleByIdAsync<MakeViewModel>(NonExistentId));
        }

        [Theory]
        [InlineData(TestName1TestId)]
        [InlineData(TestName2TestId)]
        [InlineData(TestName3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenMakeExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenMakeDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedMakes()
        {
            this.context.Makes.AddRange(
                            new Make { Id = TestName1TestId, Name = TestName1 },
                            new Make { Id = TestName2TestId, Name = TestName2 },
                            new Make { Id = TestName3TestId, Name = TestName3 });
            this.context.SaveChanges();
        }
    }
}
