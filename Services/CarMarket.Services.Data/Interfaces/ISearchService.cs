using CarMarket.Services.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Services.Data.Interfaces
{
    public interface ISearchService
    {
        IReadOnlyDictionary<int, string> GetOrderingValues();

        Task<IEnumerable<T>> GetSearchResultAsync<T>(SearchModelDto searchModel);
    }
}
