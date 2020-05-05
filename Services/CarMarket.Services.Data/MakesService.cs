namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class MakesService : IMakesService
    {
        private readonly IRepository<Make> makesRepository;
        private readonly IMapper mapper;

        public MakesService(IRepository<Make> makesRepository, IMapper mapper)
        {
            this.makesRepository = makesRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var makes = this.makesRepository
                .AllAsNoTracking()
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(makes);
        }
    }
}
