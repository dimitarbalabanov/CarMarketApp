namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFuelsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<string> GetFuelTypeByIdAsync(int? id);
    }
}
