namespace CarMarket.Services.Data
{
    using System.Threading.Tasks;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Http;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> imagesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ImagesService(IRepository<Image> imagesRepository, ICloudinaryService cloudinaryService)
        {
            this.imagesRepository = imagesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Image> UploadAsync<T>(IFormFile file, bool isMain)
        {
            var uploadResult = await this.cloudinaryService.UploadImageAsync(file, file.Name);
            var image = new Image
            {
                //ImageUrl = uploadResult.SecureUri.AbsoluteUri,
                //PublicId = uploadResult.PublicId,
                IsMain = isMain,
            };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();
            return image;
        }
    }
}
