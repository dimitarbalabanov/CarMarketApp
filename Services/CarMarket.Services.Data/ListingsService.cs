namespace CarMarket.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ListingsService : IListingsService
    {
        private const int LatestListingsCount = 8;

        private readonly IRepository<Listing> listingsRepository;
        private readonly IMapper mapper;
        private readonly IImagesService imagesService;

        public ListingsService(
            IRepository<Listing> listingsRepository,
            IMapper mapper,
            IImagesService imagesService)
        {
            this.listingsRepository = listingsRepository;
            this.mapper = mapper;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync<T>(T model, string userId, IFormFile mainImage, IFormFile secImageA, IFormFile secImageB)
        {
            var mainImg = await this.imagesService.UploadAsync(mainImage, true);
            var secImgA = await this.imagesService.UploadAsync(secImageA);
            var secImgB = await this.imagesService.UploadAsync(secImageB);

            var listing = this.mapper.Map<Listing>(model);
            listing.SellerId = userId;
            listing.Images = new List<Image> { mainImg, secImgA, secImgB };

            await this.listingsRepository.AddAsync(listing);
            await this.listingsRepository.SaveChangesAsync();
            return listing.Id;
        }

        public async Task<int> EditAsync<T>(T model, int listingId, string userId)
        {
            var listingFromDb = await this.listingsRepository.All().FirstOrDefaultAsync(l => l.Id == listingId);
            var newListing = this.mapper.Map<Listing>(model);

            listingFromDb.BodyId = newListing.BodyId;
            listingFromDb.ColorId = newListing.ColorId;
            listingFromDb.ConditionId = newListing.ConditionId;
            listingFromDb.CreatedOn = DateTime.UtcNow;
            listingFromDb.Description = newListing.Description;
            listingFromDb.FuelId = newListing.FuelId;
            listingFromDb.Horsepower = newListing.Horsepower;
            listingFromDb.Mileage = newListing.Mileage;
            listingFromDb.ModelId = newListing.ModelId;
            listingFromDb.Price = newListing.Price;
            listingFromDb.ProductionYear = newListing.ProductionYear;
            listingFromDb.TransmissionId = newListing.TransmissionId;

            this.listingsRepository.Update(listingFromDb);
            await this.listingsRepository.SaveChangesAsync();
            return listingFromDb.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var listing = await this.listingsRepository
                .All()
                .FirstOrDefaultAsync(l => l.Id == id);

            await this.imagesService.DeleteAllImagesByListingIdAsync(listing.Id);

            this.listingsRepository.Delete(listing);
            await this.listingsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllByCreatorIdAsync<T>(string creatorId)
        {
            var listingsQuery = this.listingsRepository
                .AllAsNoTracking()
                .Where(l => l.SellerId == creatorId)
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images)
                .OrderByDescending(l => l.CreatedOn);

            var listings = await this.mapper.ProjectTo<T>(listingsQuery).ToListAsync();
            return listings;
        }

        public async Task<IEnumerable<T>> GetLatestAsync<T>()
        {
            var listingsQuery = this.listingsRepository
                .AllAsNoTracking()
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images)
                .OrderByDescending(x => x.CreatedOn)
                .Take(LatestListingsCount);

            var listings = await this.mapper.ProjectTo<T>(listingsQuery).ToListAsync();
            return listings;
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

        public async Task<int> GetTotalCountAsync()
        {
            var count = await this.listingsRepository
                .AllAsNoTracking()
                .CountAsync();

            return count;
        }
    }
}
