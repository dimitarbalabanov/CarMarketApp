namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class TransmissionsService : ITransmissionsService, IHaveValidValue
    {
        private readonly IRepository<Transmission> transmissionsRepository;
        private readonly IMapper mapper;

        public TransmissionsService(IRepository<Transmission> transmissionsRepository, IMapper mapper)
        {
            this.transmissionsRepository = transmissionsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var query = this.transmissionsRepository.AllAsNoTracking();
            var transmissions = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return transmissions;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.transmissionsRepository.
                AllAsNoTracking()
                .AnyAsync(t => t.Id == id);
            return isValid;
        }
    }
}
