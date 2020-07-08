namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITransmissionsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<string> GetTransmissionTypeByIdAsync(int? id);
    }
}
