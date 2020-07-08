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

    public class ConditionsService : IConditionsService, IHaveValidValue
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
            var query = this.conditionsRepository.AllAsNoTracking();
            var conditions = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return conditions;
        }

        public async Task<string> GetConditionTypeByIdAsync(int? id)
        {
            var conditionType = await this.conditionsRepository.AllAsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => c.Type)
                .FirstOrDefaultAsync();
            return conditionType;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.conditionsRepository.AllAsNoTracking().AnyAsync(c => c.Id == id);
            return isValid;
        }
    }
}
