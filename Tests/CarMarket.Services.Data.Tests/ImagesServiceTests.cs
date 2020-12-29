namespace CarMarket.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Tests.Common;

    using Microsoft.AspNetCore.Http;

    using Moq;
    using Xunit;

    public class ImagesServiceTests
    {
        private const string UploadTestAbsoluteUri = "testUri";
        private const string UploadTestPublicId = "testPublicId";
        private const bool UploadTestIsMain = true;
        private const int TestNonExistentImageId = 123456;
        private const int TestListingId = 1;
        private const int TestNonExistentListingId = 12345;
        private const int TotalSeededImages = 1;

        private readonly ApplicationDbContext context;
        private ImagesService sut;

        public ImagesServiceTests()
        {
            this.context = InMemoryDbContextFactory.Create();
            this.SeedData();

            var cloudinaryServiceMock = new Mock<ICloudinaryService>();
            cloudinaryServiceMock
                .Setup(c => c.UploadImageAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(new UploadResultDto { AbsoluteUri = UploadTestAbsoluteUri, PublicId = UploadTestPublicId });

            this.sut = new ImagesService(new EfRepository<Image>(this.context), cloudinaryServiceMock.Object);
        }

        [Fact]
        public async Task UploadAsync_ShouldCorrectlyCreateANewImage()
        {
            var expected = new Image
            {
                ImageUrl = UploadTestAbsoluteUri,
                PublicId = UploadTestPublicId,
                IsMain = UploadTestIsMain,
            };
            var actual = await this.sut.UploadAsync(new Mock<IFormFile>().Object, true);

            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
            Assert.Equal(expected.PublicId, actual.PublicId);
            Assert.Equal(expected.IsMain, actual.IsMain);
        }

        [Fact]
        public async Task DeleteAllImagesByListingIdAsync_ShouldCorrectlyDeleteAllImages_WithValidListingId()
        {
            var expectedCount = 0;
            await this.sut.DeleteAllImagesByListingIdAsync(TestListingId);

            Assert.Equal(expectedCount, this.context.Images.Count());
        }

        [Fact]
        public async Task ChangeImageByIdAsync_ShouldThrowCustomNotFoundException_WithGivenInvalidId()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => this.sut.ChangeImageByIdAsync(TestNonExistentImageId, It.IsAny<IFormFile>(), It.IsAny<bool>()));
        }

        [Fact]
        public async Task DeleteAllImagesByListingIdAsync_ShouldNotDoAnything_WithInvalidListingId()
        {
            await this.sut.DeleteAllImagesByListingIdAsync(TestNonExistentListingId);

            Assert.Equal(TotalSeededImages, this.context.Images.Count());
        }

        private void SeedData()
        {
            this.context.Images.AddRange(
                new Image { ListingId = TestListingId });
            this.context.SaveChanges();
        }
    }
}
