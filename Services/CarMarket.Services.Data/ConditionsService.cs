namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class ConditionsService : IConditionsService
    {
        private readonly IRepository<Condition> conditionsRepository;
        private readonly IMapper mapper;

        public ConditionsService(IRepository<Condition> conditionsRepository, IMapper mapper)
        {
            this.conditionsRepository = conditionsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var conditions = this.conditionsRepository
                .AllAsNoTracking()
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(conditions);
        }
    }
}
