namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class FuelsService : IFuelsService, IHaveValidValue
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
            var query = this.fuelsRepository.AllAsNoTracking();
            var fuels = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return fuels;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.fuelsRepository.AllAsNoTracking().AnyAsync(m => m.Id == id);
            return isValid;
        }
    }
}
