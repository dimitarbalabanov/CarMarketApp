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
        private const int TestId = 1;

        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public TransmissionsServiceTests()
        {
            this.mapper = MapperFactory.Create();
            this.context = InMemoryDbContextFactory.Create();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectValues_WhenValuesExist()
        {
            await this.SeedMultiple();
            var service = this.CreateRepositoryAndService();

            var expected = new List<TransmissionSelectListViewModel>
            {
                new TransmissionSelectListViewModel { Type = TestType1 },
                new TransmissionSelectListViewModel { Type = TestType2 },
                new TransmissionSelectListViewModel { Type = TestType3 },
            };

            var actual = (await service.GetAllAsync<TransmissionSelectListViewModel>()).ToList();

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
            var actual = await service.GetAllAsync<TransmissionSelectListViewModel>();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetTransmissionTypeByIdAsync_ShouldReturnCorrectType_WhenTransmissionExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetTransmissionTypeByIdAsync(TestId);

            Assert.Equal(TestType1, actual);
        }

        [Fact]
        public async Task GetTransmissionTypeByIdAsync_ShouldReturnNull_WhenTransmissionDoesNotExist()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.GetTransmissionTypeByIdAsync(TestId);

            Assert.Null(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnTrue_WhenTransmissionExists()
        {
            await this.SeedSingle();
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.True(actual);
        }

        [Fact]
        public async Task IsValidByIdAsync_ShouldReturnFalse_WhenTransmissionDoesNotExists()
        {
            var service = this.CreateRepositoryAndService();
            var actual = await service.IsValidByIdAsync(TestId);

            Assert.False(actual);
        }

        private TransmissionsService CreateRepositoryAndService()
        {
            var repository = new EfRepository<Transmission>(this.context);
            var service = new TransmissionsService(repository, this.mapper);
            return service;
        }

        private async Task SeedMultiple()
        {
            await this.context.Transmissions.AddRangeAsync(
                new Transmission { Type = TestType1 },
                new Transmission { Type = TestType2 },
                new Transmission { Type = TestType3 });
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSingle()
        {
            await this.context.Transmissions.AddAsync(new Transmission { Id = TestId, Type = TestType1 });
            await this.context.SaveChangesAsync();
        }
    }
}
