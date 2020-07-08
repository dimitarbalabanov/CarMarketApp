namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Pagination;

    public interface ISearchService
    {
        IReadOnlyDictionary<int, string> GetOrderingValues { get; }

        Task<PaginatedList<T>> GetSearchResultAsync<T>(SearchModelDto searchModel, int pageNumber);

        Task<T> ConstructSearchInfoModelAsync<T>(SearchModelDto searchInput);
    }
}
