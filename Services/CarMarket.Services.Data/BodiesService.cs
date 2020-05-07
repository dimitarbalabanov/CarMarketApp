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

    public class BodiesService : IBodiesService
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
           var bodies = await this.bodiesRepository
                .AllAsNoTracking()
                .ToListAsync();

           return this.mapper.Map<IEnumerable<T>>(bodies);
        }
    }
}
