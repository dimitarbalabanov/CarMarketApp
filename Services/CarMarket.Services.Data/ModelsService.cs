namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class ModelsService : IModelsService
    {
        private readonly IRepository<Model> modelsRepository;
        private readonly IMapper mapper;

        public ModelsService(IRepository<Model> modelsRepository, IMapper mapper)
        {
            this.modelsRepository = modelsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAllByMakeId<T>(int id)
        {
            var models = this.modelsRepository
                .AllAsNoTracking()
                .Where(m => m.MakeId == id)
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(models);
        }
    }
}
