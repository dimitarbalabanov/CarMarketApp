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

    public class FuelsService : IFuelsService
    {
        private readonly IRepository<Fuel> fuelsRepository;
        private readonly IMapper mapper;

        public FuelsService(IRepository<Fuel> fuelsRepository, IMapper mapper)
        {
            this.fuelsRepository = fuelsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var fuels = await this.fuelsRepository
                .AllAsNoTracking()
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(fuels);
        }
    }
}
