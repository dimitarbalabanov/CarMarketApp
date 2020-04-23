namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class TransmissionService : ITransmissionService
    {
        private readonly IRepository<Transmission> transmissionsRepository;
        private readonly IMapper mapper;

        public TransmissionService(IRepository<Transmission> transmissionsRepository, IMapper mapper)
        {
            this.transmissionsRepository = transmissionsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var transmissions = this.transmissionsRepository
                .AllAsNoTracking()
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(transmissions);
        }
    }
}
