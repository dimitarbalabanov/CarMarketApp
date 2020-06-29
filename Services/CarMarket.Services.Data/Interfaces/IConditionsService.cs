namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConditionsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
