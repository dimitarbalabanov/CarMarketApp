namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class FuelService : IFuelService
    {
        private readonly IRepository<Fuel> fuelsRepository;
        private readonly IMapper mapper;

        public FuelService(IRepository<Fuel> fuelsRepository, IMapper mapper)
        {
            this.fuelsRepository = fuelsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var fuels = this.fuelsRepository
                .AllAsNoTracking()
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(fuels);
        }
    }
}
