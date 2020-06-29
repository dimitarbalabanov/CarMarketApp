namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<int> GetTotalCountAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetUserInfoByIdAsync<T>(string userId);
    }
}
