namespace CarMarket.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> imagesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ImagesService(IRepository<Image> imagesRepository, ICloudinaryService cloudinaryService)
        {
            this.imagesRepository = imagesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<Image> UploadAsync(IFormFile file, bool isMain)
        {
            var uploadResult = await this.cloudinaryService.UploadImageAsync(file, file.Name);
            var image = new Image
            {
                ImageUrl = uploadResult.SecureUri.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                IsMain = isMain,
            };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();
            return image;
        }

        public async Task DeleteAllImagesByListingIdAsync(int listingId)
        {
            var images = await this.imagesRepository.All()
                .Where(i => i.ListingId == listingId)
                .ToListAsync();

            foreach (var img in images)
            {
                this.imagesRepository.Delete(img);
                await this.cloudinaryService.DestroyImageAsync(img.PublicId);
            }

            await this.imagesRepository.SaveChangesAsync();
        }
    }
}
