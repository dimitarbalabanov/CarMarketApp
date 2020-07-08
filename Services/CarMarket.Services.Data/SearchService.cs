namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
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
        private readonly IMakesService makesService;
        private readonly IModelsService modelsService;
        private readonly IFuelsService fuelsService;
        private readonly ITransmissionsService transmissionsService;
        private readonly IColorsService colorsService;
        private readonly IConditionsService conditionsService;
        private readonly IBodiesService bodiesService;
        private readonly IMapper mapper;

        public SearchService(
            IRepository<Listing> listingsRepository,
            IMakesService makesService,
            IModelsService modelsService,
            IFuelsService fuelsService,
            ITransmissionsService transmissionsService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IBodiesService bodiesService,
            IMapper mapper)
        {
            this.listingsRepository = listingsRepository;
            this.makesService = makesService;
            this.modelsService = modelsService;
            this.fuelsService = fuelsService;
            this.transmissionsService = transmissionsService;
            this.colorsService = colorsService;
            this.conditionsService = conditionsService;
            this.bodiesService = bodiesService;
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

            if (!OrderingValues.ContainsKey(searchModel.OrderingValue))
            {
                searchModel.OrderingValue = 0;
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

        public async Task<T> ConstructSearchInfoModelAsync<T>(SearchModelDto searchInput)
        {
            var searchInfoModel = new SearchInfoModelDto();
            searchInfoModel.OrderingValueString = OrderingValues[searchInput.OrderingValue];
            searchInfoModel.BodyType = await this.bodiesService.GetBodyTypeByIdAsync(searchInput.BodyId);
            searchInfoModel.ColorName = await this.colorsService.GetColorNameByIdAsync(searchInput.ColorId);
            searchInfoModel.ConditionType = await this.conditionsService.GetConditionTypeByIdAsync(searchInput.ConditionId);
            searchInfoModel.FuelType = await this.fuelsService.GetFuelTypeByIdAsync(searchInput.FuelId);
            searchInfoModel.MakeName = await this.makesService.GetMakeNameByIdAsync(searchInput.MakeId);
            searchInfoModel.ModelName = await this.modelsService.GetModelNameByIdAsync(searchInput.ModelId);
            searchInfoModel.TransmissionType = await this.transmissionsService.GetTransmissionTypeByIdAsync(searchInput.TransmissionId);

            var model = this.mapper.Map<T>(searchInfoModel);
            return model;
        }
    }
}
