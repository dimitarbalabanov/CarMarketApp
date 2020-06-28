namespace CarMarket.Services.Data.SearchServiceHelpers
{
    using System.Collections.Generic;
    using System.Linq;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;

    internal static class SearchFieldMutatorsProvider
    {
        public static readonly IEnumerable<SearchFieldMutator<Listing, SearchModelDto>> SearchFieldMutators = new List<SearchFieldMutator<Listing, SearchModelDto>>
        {
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.BodyId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.BodyId == searchModel.BodyId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.ColorId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.ColorId == searchModel.ColorId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.ConditionId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.ConditionId == searchModel.ConditionId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.FuelId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.FuelId == searchModel.FuelId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.MakeId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.MakeId == searchModel.MakeId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.ModelId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.ModelId == searchModel.ModelId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.TransmissionId.HasValue,
                    (listings, searchModel) => listings.Where(l => l.TransmissionId == searchModel.TransmissionId))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.HorsepowerFrom.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Horsepower >= searchModel.HorsepowerFrom))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.HorsepowerTo.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Horsepower <= searchModel.HorsepowerTo))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.MileageFrom.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Mileage >= searchModel.MileageFrom))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.MileageTo.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Mileage <= searchModel.MileageTo))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.PriceFrom.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Price >= searchModel.PriceFrom))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.PriceTo.HasValue,
                    (listings, searchModel) => listings.Where(l => l.Price <= searchModel.PriceTo))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.ProductionYearFrom.HasValue,
                    (listings, searchModel) => listings.Where(l => l.ProductionYear >= searchModel.ProductionYearFrom))
            },
            {
                new SearchFieldMutator<Listing, SearchModelDto>(
                    searchModel => searchModel.ProductionYearTo.HasValue,
                    (listings, searchModel) => listings.Where(l => l.ProductionYear <= searchModel.ProductionYearTo))
            },
        };
    }
}
