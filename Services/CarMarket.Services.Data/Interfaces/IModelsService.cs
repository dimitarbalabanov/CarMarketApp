namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelsService
    {
        Task<IEnumerable<T>> GetAllByMakeIdAsync<T>(int id);

        Task<string> GetModelNameByIdAsync(int? id);

        Task<bool> IsValidByMakeIdAndIdAsync(int makeId, int modelId);

        Task<bool> ExistsByNameAsync(string name);

        Task CreateAsync<T>(T inputModel);
    }
}
