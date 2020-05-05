namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    public class ColorsService : IColorsService
    {
        private readonly IRepository<Color> colorsRepository;
        private readonly IMapper mapper;

        public ColorsService(IRepository<Color> colorsRepository, IMapper mapper)
        {
            this.colorsRepository = colorsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var colors = this.colorsRepository
                .AllAsNoTracking()
                .ToList();

            return this.mapper.Map<IEnumerable<T>>(colors);
        }
    }
}
