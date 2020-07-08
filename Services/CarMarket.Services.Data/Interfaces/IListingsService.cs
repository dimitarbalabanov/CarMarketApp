namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Dtos;

    public interface IListingsService
    {
        Task<int> CreateAsync<T>(T model, string userId, IEnumerable<CreateListingInputImageDto> inputImages);

        Task<int> EditAsync<T>(T model, int listingId, string userId, IEnumerable<EditListingInputImageDto> inputImages);

        Task<T> GetSingleByIdAsync<T>(int id);

        Task<bool> HasPermissionToAccessAsync(string userId, int listingId);

        Task<IEnumerable<T>> GetLatestAsync<T>();

        Task<IEnumerable<T>> GetAllByCreatorIdAsync<T>(string creatorId);

        Task DeleteByIdAsync(int listingId, string userId);

        Task<int> GetTotalCountAsync();
    }
}
