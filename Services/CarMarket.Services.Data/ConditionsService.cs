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

    public class ConditionsService : IConditionsService
    {
        private readonly IRepository<Condition> conditionsRepository;
        private readonly IMapper mapper;

        public ConditionsService(IRepository<Condition> conditionsRepository, IMapper mapper)
        {
            this.conditionsRepository = conditionsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var conditions = await this.conditionsRepository
                .AllAsNoTracking()
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(conditions);
        }
    }
}
