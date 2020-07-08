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

    public class BodiesService : IBodiesService, IHaveValidValue
    {
        private readonly IRepository<Body> bodiesRepository;
        private readonly IMapper mapper;

        public BodiesService(IRepository<Body> bodiesRepository, IMapper mapper)
        {
            this.bodiesRepository = bodiesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var query = this.bodiesRepository.AllAsNoTracking();
            var bodies = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return bodies;
        }

        public async Task<string> GetBodyTypeByIdAsync(int? id)
        {
            var bodyType = await this.bodiesRepository.AllAsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => b.Type)
                .FirstOrDefaultAsync();
            return bodyType;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.bodiesRepository.AllAsNoTracking().AnyAsync(b => b.Id == id);
            return isValid;
        }
    }
}
