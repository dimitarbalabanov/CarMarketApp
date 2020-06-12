﻿namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Services.Data.Pagination;
    using CarMarket.Services.Data.SearchServiceHelpers;
    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private const int DefaultPageSize = 5;
        private static readonly IReadOnlyDictionary<int, string> OrderingValues = new Dictionary<int, string>
        {
            { 1, "Newest" },
            { 2, "Oldest" },
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

        public async Task<PaginatedList<T>> GetSearchResultAsync<T>(SearchModelDto searchModel, int pageNumber)
        {
            var listings = this.listingsRepository.AllAsNoTracking();

            foreach (var mutator in SearchFieldMutatorsProvider.SearchFieldMutators)
            {
                listings = mutator.Apply(searchModel, listings);
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
