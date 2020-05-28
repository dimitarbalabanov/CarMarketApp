namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class TransmissionsService : ITransmissionsService
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
            var transmissions = await this.transmissionsRepository
                .AllAsNoTracking()
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(transmissions);
        }
    }
}
