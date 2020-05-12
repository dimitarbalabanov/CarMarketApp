namespace CarMarket.Services.Data
{
    using AutoMapper;
    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<T>> GetAllByCreatorIdAsync<T>(string creatorId)
        {
            var listings = await this.listingsRepository
                .AllAsNoTracking()
                .Where(l => l.SellerId == creatorId)
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images)
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(listings);
        }

        public async Task<IEnumerable<T>> GetLatestAsync<T>()
        {
            var listings = await this.listingsRepository
                .AllAsNoTracking()
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images)
                .OrderByDescending(x => x.CreatedOn)
                .Take(8)
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(listings);
        }

        public async Task<T> GetSingleByIdAsync<T>(int id)
        {
            var listing = await this.listingsRepository
                .AllAsNoTracking()
                .Include(l => l.Body)
                .Include(l => l.Color)
                .Include(l => l.Condition)
                .Include(l => l.Fuel)
                .Include(l => l.Images)
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Seller)
                .Include(l => l.Transmission)
                .FirstOrDefaultAsync(l => l.Id == id);

            return this.mapper.Map<T>(listing);
        }

    }
}
