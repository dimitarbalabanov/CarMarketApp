namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class MakesService : IMakesService
    {
        private readonly IRepository<Make> makesRepository;
        private readonly IMapper mapper;

        public MakesService(IRepository<Make> makesRepository, IMapper mapper)
        {
            this.makesRepository = makesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var makes = await this.makesRepository
                .AllAsNoTracking()
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(makes);
        }
    }
}
