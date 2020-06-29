namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMakesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync<T>(T model);

        Task<bool> ExistsByNameAsync(string name);

        Task<T> GetSingleByIdAsync<T>(int id);
    }
}
