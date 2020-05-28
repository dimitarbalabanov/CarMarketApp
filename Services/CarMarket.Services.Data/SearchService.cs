namespace CarMarket.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly Dictionary<int, string> orderingValues = new Dictionary<int, string>
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

        public async Task<IEnumerable<T>> GetSearchResultAsync<T>(SearchModelDto searchModel)
        {
            var listings = this.listingsRepository.AllAsNoTracking();
            if (searchModel.BodyId.HasValue)
            {
                listings = listings.Where(l => l.BodyId == searchModel.BodyId);
            }

            if (searchModel.ColorId.HasValue)
            {
                listings = listings.Where(l => l.ColorId == searchModel.ColorId);
            }

            if (searchModel.ConditionId.HasValue)
            {
                listings = listings.Where(l => l.ConditionId == searchModel.ConditionId);
            }

            if (searchModel.FuelId.HasValue)
            {
                listings = listings.Where(l => l.FuelId == searchModel.FuelId);
            }

            if (searchModel.MakeId.HasValue)
            {
                listings = listings.Where(l => l.MakeId == searchModel.MakeId);
            }

            if (searchModel.ModelId.HasValue)
            {
                listings = listings.Where(l => l.ModelId == searchModel.ModelId);
            }

            if (searchModel.TransmissionId.HasValue)
            {
                listings = listings.Where(l => l.TransmissionId == searchModel.TransmissionId);
            }

            if (searchModel.HorsepowerFrom.HasValue)
            {
                listings = listings.Where(l => l.Horsepower >= searchModel.HorsepowerFrom);
            }

            if (searchModel.HorsepowerTo.HasValue)
            {
                listings = listings.Where(l => l.Horsepower <= searchModel.HorsepowerTo);
            }

            if (searchModel.MileageFrom.HasValue)
            {
                listings = listings.Where(l => l.Mileage >= searchModel.MileageFrom);
            }

            if (searchModel.MileageTo.HasValue)
            {
                listings = listings.Where(l => l.Mileage <= searchModel.MileageTo);
            }

            if (searchModel.PriceFrom.HasValue)
            {
                listings = listings.Where(l => l.Price >= searchModel.PriceFrom);
            }

            if (searchModel.PriceTo.HasValue)
            {
                listings = listings.Where(l => l.Price <= searchModel.PriceTo);
            }

            if (searchModel.ProductionYearFrom.HasValue)
            {
                listings = listings.Where(l => l.ProductionYear >= searchModel.ProductionYearFrom);
            }

            if (searchModel.ProductionYearTo.HasValue)
            {
                listings = listings.Where(l => l.ProductionYear <= searchModel.ProductionYearTo);
            }

            switch (searchModel.OrderingValue)
            {
                case 1:
                    listings = listings.OrderBy(l => l.CreatedOn);
                    break;
                case 2:
                    listings = listings.OrderByDescending(l => l.CreatedOn);
                    break;
                case 3:
                    listings = listings.OrderBy(l => l.Price);
                    break;
                case 4:
                    listings = listings.OrderByDescending(l => l.Price);
                    break;
                case 5:
                    listings = listings.OrderBy(l => l.ProductionYear);
                    break;
                case 6:
                    listings = listings.OrderByDescending(l => l.ProductionYear);
                    break;
                case 7:
                    listings = listings.OrderBy(l => l.Mileage);
                    break;
                case 8:
                    listings = listings.OrderByDescending(l => l.Mileage);
                    break;
                default:
                    break;
            }

            var asdf = this.listingsRepository.AllAsNoTracking().Where(IsCorrect(searchModel));
            var listingsResult = await listings.ToListAsync();
            var bla = IsCorrect(searchModel);
            return this.mapper.Map<IEnumerable<T>>(listingsResult);
        }

        public IReadOnlyDictionary<int, string> GetOrderingValues()
        {
            return this.orderingValues;
        }

        private static BinaryExpression BuildPredicate(SearchModelDto model)
        {
            var type = typeof(Listing);
            var prop = nameof(model.BodyId);
            Predicate<int> asdf = x => x == 3;
            BinaryExpression predicate = Expression.Equal(
                Expression.Constant(model.BodyId, typeof(int?)),
                Expression.Constant(model.BodyId, typeof(int?)));
            return predicate;
        }

        public static Expression<Func<Listing, bool>> IsCorrect(SearchModelDto model)
        {
            return l => l.BodyId == model.BodyId;
        }
    }
}
