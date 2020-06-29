namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ColorsService : IColorsService, IHaveValidValue
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
            var query = this.colorsRepository.AllAsNoTracking();
            var colors = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return colors;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.colorsRepository.
                AllAsNoTracking()
                .AnyAsync(c => c.Id == id);
            return isValid;
        }
    }
}
