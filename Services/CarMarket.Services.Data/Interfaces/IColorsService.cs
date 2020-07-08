namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IColorsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<string> GetColorNameByIdAsync(int? id);
    }
}
