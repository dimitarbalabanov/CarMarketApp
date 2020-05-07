namespace CarMarket.Services.Data
{
    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ListingsService : IListingsService
    {
        private readonly IRepository<Listing> listingsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ListingsService(IRepository<Listing> listingsRepository, ICloudinaryService cloudinaryService)
        {
            this.listingsRepository = listingsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(
            string userId,
            int makeId,
            int modelId,
            int bodyId,
            int transmissionId,
            int fuelId,
            int conditionId,
            int colorId,
            int productionYear,
            int mileage,
            int horsepower,
            decimal price,
            string description,
            IEnumerable<IFormFile> images)
        {
            var listing = new Listing
            {
                BodyId = bodyId,
                ColorId = colorId,
                ConditionId = conditionId,
                Description = description,
                FuelId = fuelId,
                Horsepower = horsepower,
                Mileage = mileage,
                ModelId = modelId,
                Price = price,
                ProductionYear = productionYear,
                SellerId = userId,
                TransmissionId = transmissionId,
            };

            var imageUrl = await this.cloudinaryService.UploadImage(images.First(), "image");
            var image = new Image
            {
                ImageUrl = imageUrl,
            };
            listing.Images.Add(image);

            await this.listingsRepository.AddAsync(listing);
            await this.listingsRepository.SaveChangesAsync();
            return listing.Id;
        }
    }
}
