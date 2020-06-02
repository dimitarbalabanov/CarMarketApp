namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ListingsService : IListingsService
    {
        private readonly IRepository<Listing> listingsRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;
        private readonly IRepository<Image> imageRepository;

        public ListingsService(
            IRepository<Listing> listingsRepository,
            ICloudinaryService cloudinaryService,
            IMapper mapper,
            IRepository<Image> imageRepository)
        {
            this.listingsRepository = listingsRepository;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        public async Task<int> CreateAsync<T>(T model, string userId, IEnumerable<IFormFile> images)
        {
            // for now they can be nullable
            var imageUrls = images?
                .Select(async i => await this.cloudinaryService.UploadImageAsync(i, i.FileName))
                .Select(i => i.Result)
                .ToList();

            var listingImages = imageUrls?
                .Select(url => new Image { ImageUrl = url })
                .ToList();

            var listing = this.mapper.Map<Listing>(model);
            listing.SellerId = userId;
            listing.Images = listingImages;

            await this.listingsRepository.AddAsync(listing);
            await this.listingsRepository.SaveChangesAsync();
            return listing.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var listing = await this.listingsRepository
                .All()
                .FirstOrDefaultAsync(l => l.Id == id);
            var images = await this.imageRepository.All().Where(i => i.ListingId == id).ToListAsync();
            foreach (var img in images)
            {
                this.imageRepository.Delete(img);
            }

            // remove from cloudinary??
            await this.imageRepository.SaveChangesAsync();

            this.listingsRepository.Delete(listing);
            await this.listingsRepository.SaveChangesAsync();
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
