namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.SearchServiceHelpers;
    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private static readonly IReadOnlyDictionary<int, string> OrderingValues = new Dictionary<int, string>
        {
            { 1, "Oldest" },
            { 2, "Newest" },
            { 3, "Price (ascending)" },
            { 4, "Price (descending)" },
            { 5, "Production year (ascending)" },
            { 6, "Production year (descending)" },
            { 7, "Mileage (ascending)" },
            { 8, "Mileage (descending)" },
        };

        private readonly IRepository<Listing> listingsRepository;
        private readonly IMapper mapper;

        public SearchService(IRepository<Listing> listingsRepository, IMapper mapper)
        {
            this.listingsRepository = listingsRepository;
            this.mapper = mapper;
        }

        public IReadOnlyDictionary<int, string> GetOrderingValues => OrderingValues;

        public async Task<IEnumerable<T>> GetSearchResultAsync<T>(SearchModelDto searchModel)
        {
            var listings = this.listingsRepository.AllAsNoTracking();
            foreach (var mutator in SearchFieldMutatorsProvider.SearchFieldMutators)
            {
                listings = mutator.Apply(searchModel, listings);
            }

            listings = OrderingMutatorsProvider.OrderingMutators[searchModel.OrderingValue](listings);
            var searchResult = await listings.ToListAsync();
            return this.mapper.Map<IEnumerable<T>>(searchResult);
        }
    }
}
