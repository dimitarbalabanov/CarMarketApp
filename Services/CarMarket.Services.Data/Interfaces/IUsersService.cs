using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Services.Data.Interfaces
{
    public interface IUsersService
    {
        Task<int> GetTotalCountAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetUserInfoByIdAsync<T>(string userId);
    }
}
