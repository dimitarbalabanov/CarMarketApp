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
    using CarMarket.Web.ViewModels.Administration.Makes;
    using CarMarket.Web.ViewModels.Models;

    using Xunit;

    public class ModelsServiceTests
    {
        private const string TestMake1 = "Audi";
        private const string TestMake1Model1 = "A1";
        private const string TestMake1Model2 = "A2";
        private const string TestMake1Model3 = "A3";
        private const int TestMake1TestId = 1;
        private const int TestMake1Model1TestId = 1;
        private const int TestMake1Model2TestId = 2;
        private const int TestMake1Model3TestId = 3;

        private const string TestMake2 = "BMW";
        private const string TestMake2Model1 = "M3";
        private const string TestMake2Model2 = "M4";
        private const string TestMake2Model3 = "M5";
        private const int TestMake2TestId = 2;
        private const int TestMake2Model1TestId = 4;
        private const int TestMake2Model2TestId = 5;
        private const int TestMake2Model3TestId = 6;

        private const string TestCreateModelName = "Golf";
        private const string NonExistentModelName = "NoName";
        private const int NonExistentModelId = 1234567;
        private const int NonExistentMakeId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly ModelsService sut;

        public ModelsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedModels();
            this.sut = new ModelsService(new EfRepository<Model>(this.context), this.mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldCorrectlyCreateAModel()
        {
            var input = new CreateModelInputModel { Name = TestCreateModelName, MakeId = TestMake1TestId };
            await this.sut.CreateAsync<CreateModelInputModel>(input);
            var createdModel = this.context.Models.FirstOrDefault(m => m.Name == TestCreateModelName);

            Assert.NotNull(createdModel);
        }

        [Theory]
        [InlineData(TestMake1Model1)]
        [InlineData(TestMake1Model2)]
        [InlineData(TestMake1Model3)]
        [InlineData(TestMake2Model1)]
        [InlineData(TestMake2Model2)]
        [InlineData(TestMake2Model3)]
        public async Task ExistsByNameAsync_ShouldReturnTrue_WhenModelWithGivenNameExists(string name)
        {
            var actual = await this.sut.ExistsByNameAsync(name);

            Assert.True(actual);
        }

        [Fact]
        public async Task ExistsByNameAsync_ShouldReturnFalse_WhenModelWithGivenNameDoesNotExist()
        {
            var actual = await this.sut.ExistsByNameAsync(NonExistentModelName);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(TestMake1Model1TestId, TestMake1Model1)]
        [InlineData(TestMake1Model2TestId, TestMake1Model2)]
        [InlineData(TestMake1Model3TestId, TestMake1Model3)]
        [InlineData(TestMake2Model1TestId, TestMake2Model1)]
        [InlineData(TestMake2Model2TestId, TestMake2Model2)]
        [InlineData(TestMake2Model3TestId, TestMake2Model3)]
        public async Task GetModelNameByIdAsync_ShouldReturnCorrectName_WhenModelExists(int id, string expected)
        {
            var actual = await this.sut.GetModelNameByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetModelNameByIdAsync_ShouldReturnNull_WhenModelDoesNotExist()
        {
            var actual = await this.sut.GetModelNameByIdAsync(NonExistentModelId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllByMakeIdAsync_ShouldReturnCorrectValues_WhenGivenExistingMakeId()
        {
            var expected = new List<ModelSelectListViewModel>
            {
                new ModelSelectListViewModel { Name = TestMake1Model1 },
                new ModelSelectListViewModel { Name = TestMake1Model2 },
                new ModelSelectListViewModel { Name = TestMake1Model3 },
            };

            var actual = (await this.sut.GetAllByMakeIdAsync<ModelSelectListViewModel>(TestMake1TestId)).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
            }
        }

        [Fact]
        public async Task GetAllByMakeIdAsync_ShouldReturnEmptyList_WhenGivenNonExistingMakeId()
        {
            var actual = await this.sut.GetAllByMakeIdAsync<ModelSelectListViewModel>(NonExistentMakeId);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestMake1TestId, TestMake1Model1TestId)]
        [InlineData(TestMake1TestId, TestMake1Model2TestId)]
        [InlineData(TestMake1TestId, TestMake1Model3TestId)]
        [InlineData(TestMake2TestId, TestMake2Model1TestId)]
        [InlineData(TestMake2TestId, TestMake2Model2TestId)]
        [InlineData(TestMake2TestId, TestMake2Model3TestId)]
        public async Task IsValidByMakeIdAndIdAsync_ShouldReturnTrue_WhenMakeModelCombinationExists(int makeId, int modelId)
        {
            var actual = await this.sut.IsValidByMakeIdAndIdAsync(makeId, modelId);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(NonExistentMakeId, TestMake2Model1TestId)]
        [InlineData(TestMake1Model2TestId, NonExistentModelId)]
        [InlineData(TestMake1Model3TestId, TestMake2Model1TestId)]
        [InlineData(TestMake1Model1TestId, TestMake2Model1TestId)]
        [InlineData(TestMake1Model2TestId, TestMake1Model2TestId)]
        [InlineData(TestMake2Model3TestId, TestMake1Model3TestId)]
        public async Task IsValidByMakeIdAndIdAsyncc_ShouldReturnFalse_WhenMakeModelCombinationDoesNotExist(int makeId, int modelId)
        {
            var actual = await this.sut.IsValidByMakeIdAndIdAsync(makeId, modelId);

            Assert.False(actual);
        }

        private void SeedModels()
        {
            this.context.Makes.AddRange(
                new Make
                {
                    Id = TestMake1TestId,
                    Name = TestMake1,
                    Models =
                    {
                        new Model { Id = TestMake1Model1TestId, Name = TestMake1Model1 },
                        new Model { Id = TestMake1Model2TestId, Name = TestMake1Model2 },
                        new Model { Id = TestMake1Model3TestId, Name = TestMake1Model3 },
                    },
                },
                new Make
                {
                    Id = TestMake2TestId,
                    Name = TestMake2,
                    Models =
                    {
                        new Model { Id = TestMake2Model1TestId, Name = TestMake2Model1 },
                        new Model { Id = TestMake2Model2TestId, Name = TestMake2Model2 },
                        new Model { Id = TestMake2Model3TestId, Name = TestMake2Model3 },
                    },
                });
            this.context.SaveChanges();
        }
    }
}
