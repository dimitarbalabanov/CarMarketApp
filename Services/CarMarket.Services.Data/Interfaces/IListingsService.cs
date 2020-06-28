﻿namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Dtos;

    using Microsoft.AspNetCore.Http;

    public interface IListingsService
    {
        Task<int> CreateAsync<T>(T model, string userId, IEnumerable<CreateListingImageDto> inputImages);

        Task<int> EditAsync<T>(T model, int listingId, string userId, IEnumerable<EditImageDto> imgs);

        Task<T> GetSingleByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetLatestAsync<T>();

        Task<IEnumerable<T>> GetAllByCreatorIdAsync<T>(string creatorId);

        Task DeleteByIdAsync(int id);

        Task<int> GetTotalCountAsync();
    }
}
