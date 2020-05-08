using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Services.Data.Interfaces
{
    public interface IListingsService
    {
        Task<int> CreateAsync<T>(T model, string userId, IEnumerable<IFormFile> images);

        Task<T> GetSingleByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetLatestAsync<T>();
    }
}
