namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;

    public interface ISearchService
    {
        IReadOnlyDictionary<int, string> GetOrderingValues { get; }

        Task<IEnumerable<T>> GetSearchResultAsync<T>(SearchModelDto searchModel);
    }
}
