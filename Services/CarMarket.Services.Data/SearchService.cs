namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.Pagination;
    using CarMarket.Services.Data.SearchServiceHelpers;

    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private const int DefaultPageSize = 5;
        private static readonly IReadOnlyDictionary<int, string> OrderingValues = new Dictionary<int, string>
        {
            { 0, "Newest" },
            { 1, "Oldest" },
            { 2, "Price (ascending)" },
            { 3, "Price (descending)" },
            { 4, "Production year (ascending)" },
            { 5, "Production year (descending)" },
            { 6, "Mileage (ascending)" },
            { 7, "Mileage (descending)" },
        };

        private readonly IRepository<Listing> listingsRepository;
        private readonly IMapper mapper;

        public SearchService(IRepository<Listing> listingsRepository, IMapper mapper)
        {
            this.listingsRepository = listingsRepository;
            this.mapper = mapper;
        }

        public IReadOnlyDictionary<int, string> GetOrderingValues => OrderingValues;

        public async Task<PaginatedList<T>> GetSearchResultAsync<T>(SearchModelDto searchModel, string userId, int pageNumber)
        {
            var listings = this.listingsRepository.AllAsNoTracking();

            if (userId != null)
            {
                listings = listings.Where(l => l.SellerId != userId);
            }

            foreach (var mutator in SearchFieldMutatorsProvider.SearchFieldMutators)
            {
                listings = mutator.Apply(searchModel, listings);
            }

            if (!OrderingValues.ContainsKey(searchModel.OrderingValue))
            {
                throw new KeyNotFoundException();
            }

            listings = OrderingMutatorsProvider.OrderingMutators[searchModel.OrderingValue](listings);
            listings = listings
                .Include(l => l.Make)
                .Include(l => l.Model)
                .Include(l => l.Images);

            var mapperListings = this.mapper.ProjectTo<T>(listings);
            var pagedListings = await PaginatedList<T>.CreateAsync(mapperListings, pageNumber, DefaultPageSize);
            return pagedListings;
        }
    }
}
