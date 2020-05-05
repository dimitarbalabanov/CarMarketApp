namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class BodiesService : IBodiesService
    {
        private readonly IRepository<Body> bodiesRepository;
        private readonly IMapper mapper;

        public BodiesService(IRepository<Body> bodiesRepository, IMapper mapper)
        {
            this.bodiesRepository = bodiesRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
           var bodies = this.bodiesRepository
                .AllAsNoTracking()
                .ToList();

           return this.mapper.Map<IEnumerable<T>>(bodies);
        }
    }
}
