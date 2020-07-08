namespace CarMarket.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Common;
    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ListingsService : IListingsService
    {
        private const int LatestListingsCount = 8;

        private readonly IRepository<Listing> listingsRepository;
        private readonly IMapper mapper;
        private readonly IImagesService imagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListingsService(
            IRepository<Listing> listingsRepository,
            IMapper mapper,
            IImagesService imagesService,
            UserManager<ApplicationUser> userManager)
        {
            this.listingsRepository = listingsRepository;
            this.mapper = mapper;
            this.imagesService = imagesService;
            this.userManager = userManager;
        }

        public async Task<int> CreateAsync<T>(T model, string userId, IEnumerable<CreateListingInputImageDto> inputImages)
        {
            var listing = this.mapper.Map<Listing>(model);
            foreach (var img in inputImages)
            {
                var uploadedImg = await this.imagesService.UploadAsync(img.ImageFile, img.IsMain);
                listing.Images.Add(uploadedImg);
            }

            listing.SellerId = userId;
            await this.listingsRepository.AddAsync(listing);
            await this.listingsRepository.SaveChangesAsync();
            return listing.Id;
        }

        public async Task<int> EditAsync<T>(T model, int listingId, string userId, IEnumerable<EditListingInputImageDto> inputImages)
        {
            if (!(await this.IsCreatorAsync(userId, listingId) || await this.IsAdminAsync(userId)))
            {
                throw new AccessDeniedException();
            }

            var listingFromDb = await this.listingsRepository.All()
                .Include(l => l.Images)
                .FirstOrDefaultAsync(l => l.Id == listingId);

            if (listingFromDb == null)
            {
                throw new NotFoundException();
            }

            if (inputImages.Count() > 0)
            {
                foreach (var img in inputImages)
                {
                    await this.imagesService.ChangeImageByIdAsync(img.Id, img.ImageFile, img.IsMain);
                }
            }

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

        public async Task DeleteByIdAsync(int listingId, string userId)
        {
            if (!(await this.HasPermissionToAccessAsync(userId, listingId)))
            {
                throw new AccessDeniedException();
            }

            var listing = await this.listingsRepository.All()
                .FirstOrDefaultAsync(l => l.Id == listingId);
            if (listing == null)
            {
                throw new NotFoundException();
            }

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

            if (listing == null)
            {
                throw new NotFoundException();
            }

            return this.mapper.Map<T>(listing);
        }

        public async Task<int> GetTotalCountAsync()
        {
            var count = await this.listingsRepository.AllAsNoTracking().CountAsync();
            return count;
        }

        public async Task<bool> HasPermissionToAccessAsync(string userId, int listingId)
        {
            var isCreator = await this.IsCreatorAsync(userId, listingId);
            var isAdmin = await this.IsAdminAsync(userId);
            var hasPermission = isCreator || isAdmin;
            return hasPermission;
        }

        private async Task<bool> IsCreatorAsync(string userId, int listingId)
        {
            var isCreator = (await this.listingsRepository
                .AllAsNoTracking()
                .Where(l => l.Id == listingId)
                .Select(l => l.SellerId)
                .FirstOrDefaultAsync()) == userId;
            return isCreator;
        }

        private async Task<bool> IsAdminAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var isAdmin = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);
            return isAdmin;
        }
    }
}
