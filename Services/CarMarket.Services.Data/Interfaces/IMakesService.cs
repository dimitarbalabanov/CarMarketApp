﻿namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMakesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> IsValidByIdAsync(int id);
    }
}
