namespace CarMarket.Services.Data
{
    using System;
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

        public async Task<Image> UploadAsync(IFormFile file, bool isMain = false)
        {
            var uploadResult = await this.cloudinaryService.UploadImageAsync(file, file.Name);
            var image = new Image
            {
                ImageUrl = uploadResult.SecureUri.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                IsMain = isMain,
            };

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

        public async Task ChangeImageByIdAsync(int id, IFormFile file, bool isMain)
        {
            var oldImg = await this.imagesRepository.All().FirstOrDefaultAsync(i => i.Id == id);
            var newImg = await this.UploadAsync(file, isMain);
            await this.cloudinaryService.DestroyImageAsync(oldImg.PublicId);

            oldImg.ImageUrl = newImg.ImageUrl;
            oldImg.PublicId = newImg.PublicId;
            oldImg.IsMain = newImg.IsMain;
            oldImg.CreatedOn = DateTime.UtcNow;
            this.imagesRepository.Update(oldImg);
            await this.imagesRepository.SaveChangesAsync();
        }
    }
}
