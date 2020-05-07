using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Services.Data.Interfaces
{
    public interface IListingsService
    {
        Task<int> CreateAsync(
            string userId,
            int makeId,
            int modelId,
            int bodyId,
            int transmissionId,
            int fuelId,
            int conditionId,
            int colorId,
            int productionYear,
            int mileage,
            int horsepower,
            decimal price,
            string description,
            IEnumerable<IFormFile> images);
    }
}
