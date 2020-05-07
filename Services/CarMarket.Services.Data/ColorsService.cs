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

    public class ColorsService : IColorsService
    {
        private readonly IRepository<Color> colorsRepository;
        private readonly IMapper mapper;

        public ColorsService(IRepository<Color> colorsRepository, IMapper mapper)
        {
            this.colorsRepository = colorsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var colors = await this.colorsRepository
                .AllAsNoTracking()
                .ToListAsync();

            return this.mapper.Map<IEnumerable<T>>(colors);
        }
    }
}
