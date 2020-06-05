﻿namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class MakesService : IMakesService
    {
        private readonly IRepository<Make> makesRepository;
        private readonly IMapper mapper;

        public MakesService(IRepository<Make> makesRepository, IMapper mapper)
        {
            this.makesRepository = makesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var query = this.makesRepository.AllAsNoTracking();
            var makes = await this.mapper.ProjectTo<T>(query).ToListAsync();
            return makes;
        }

        public async Task<bool> IsValidByIdAsync(int id)
        {
            var isValid = await this.makesRepository.
                AllAsNoTracking()
                .AnyAsync(m => m.Id == id);
            return isValid;
        }
    }
}
