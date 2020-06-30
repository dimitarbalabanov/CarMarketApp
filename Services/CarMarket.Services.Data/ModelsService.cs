namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ModelsService : IModelsService
    {
        private readonly IRepository<Model> modelsRepository;
        private readonly IMapper mapper;

        public ModelsService(IRepository<Model> modelsRepository, IMapper mapper)
        {
            this.modelsRepository = modelsRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync<T>(T inputModel)
        {
            var model = this.mapper.Map<Model>(inputModel);
            await this.modelsRepository.AddAsync(model);
            await this.modelsRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var exists = await this.modelsRepository.AllAsNoTracking().AnyAsync(m => m.Name == name);
            return exists;
        }

        public async Task<IEnumerable<T>> GetAllByMakeIdAsync<T>(int id)
        {
            var query = this.modelsRepository.AllAsNoTracking().Where(m => m.MakeId == id);
            var models = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return models;
        }

        public async Task<bool> IsValidByMakeIdAndIdAsync(int makeId, int modelId)
        {
            var isValid = await this.modelsRepository.AllAsNoTracking().AnyAsync(mod => mod.Id == modelId && mod.MakeId == makeId);
            return isValid;
        }
    }
}
