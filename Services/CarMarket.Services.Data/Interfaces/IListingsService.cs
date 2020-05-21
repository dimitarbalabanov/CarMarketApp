namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IListingsService
    {
        Task<int> CreateAsync<T>(T model, string userId, IEnumerable<IFormFile> images);

        Task<T> GetSingleByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetLatestAsync<T>();

        Task<IEnumerable<T>> GetAllByCreatorIdAsync<T>(string creatorId);

        Task DeleteByIdAsync(int id);
    }
}
