namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Dtos;

    public interface ISearchService
    {
        IReadOnlyDictionary<int, string> GetOrderingValues();

        Task<IEnumerable<T>> GetSearchResultAsync<T>(SearchModelDto searchModel);
    }
}
