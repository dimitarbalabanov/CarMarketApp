namespace CarMarket.Services.Data
{
    using AutoMapper;
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
        private readonly IMapper mapper;

        public ListingsService(IRepository<Listing> listingsRepository, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.listingsRepository = listingsRepository;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync<T>(T model, string userId, IEnumerable<IFormFile> images)
        {
            var imageUrls = images
                .Select(async i => await this.cloudinaryService.UploadImageAsync(i, i.FileName))
                .Select(i => i.Result)
                .ToList();

            var listingImages = imageUrls
                .Select(url => new Image { ImageUrl = url })
                .ToList();

            var listing = this.mapper.Map<Listing>(model);
            listing.SellerId = userId;
            foreach (var img in listingImages)
            {
                listing.Images.Add(img);
            }

            await this.listingsRepository.AddAsync(listing);
            await this.listingsRepository.SaveChangesAsync();
            return listing.Id;
        }
    }
}
