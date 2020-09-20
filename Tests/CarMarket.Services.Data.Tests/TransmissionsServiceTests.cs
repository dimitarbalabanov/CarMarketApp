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
    using CarMarket.Web.ViewModels.Transmissions;

    using Xunit;

    public class TransmissionsServiceTests
    {
        private const string TestType1 = "Automatic";
        private const string TestType2 = "Manual";
        private const string TestType3 = "SemiAutomatic";
        private const int TestType1TestId = 1;
        private const int TestType2TestId = 2;
        private const int TestType3TestId = 3;
        private const int NonExistentId = 1234567;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly TransmissionsService sut;

        public TransmissionsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
            this.SeedTransmissions();
            this.sut = new TransmissionsService(new EfRepository<Transmission>(this.context), this.mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            var expected = new List<TransmissionSelectListViewModel>
            {
                new TransmissionSelectListViewModel { Type = TestType1 },
                new TransmissionSelectListViewModel { Type = TestType2 },
                new TransmissionSelectListViewModel { Type = TestType3 },
            };
            var actual = (await this.sut.GetAllAsync<TransmissionSelectListViewModel>()).ToList();

            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Type, actual[i].Type);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenValuesDoNotExist()
        {
            this.context.Transmissions.RemoveRange(this.context.Transmissions);
            await this.context.SaveChangesAsync();

            var actual = await this.sut.GetAllAsync<TransmissionSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(TestType1TestId, TestType1)]
        [InlineData(TestType2TestId, TestType2)]
        [InlineData(TestType3TestId, TestType3)]
        public async Task GetTransmissionTypeByIdAsync_ShouldReturnCorrectType_WhenTransmissionExists(int id, string expected)
        {
            var actual = await this.sut.GetTransmissionTypeByIdAsync(id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTransmissionTypeByIdAsync_ShouldReturnNull_WhenTransmissionDoesNotExist()
        {
            var actual = await this.sut.GetTransmissionTypeByIdAsync(NonExistentId);

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(TestType1TestId)]
        [InlineData(TestType2TestId)]
        [InlineData(TestType3TestId)]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenTransmissionExists(int id)
        {
            var actual = await this.sut.IsValidByIdAsync(id);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenTransmissionDoesNotExist()
        {
            var actual = await this.sut.IsValidByIdAsync(NonExistentId);

            Assert.False(actual);
        }

        private void SeedTransmissions()
        {
            this.context.Transmissions.AddRange(
                new Transmission { Type = TestType1 },
                new Transmission { Type = TestType2 },
                new Transmission { Type = TestType3 });
            this.context.SaveChanges();
        }
    }
}
