namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Pagination;
    using CarMarket.Services.Data.Dtos;

    public interface ISearchService
    {
        IReadOnlyDictionary<int, string> GetOrderingValues { get; }

        Task<PaginatedList<T>> GetSearchResultAsync<T>(SearchModelDto searchModel, int pageNumber);
    }
}
