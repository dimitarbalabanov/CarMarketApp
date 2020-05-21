namespace CarMarket.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IBookmarksService
    {
        Task AddAsync(string userId, int listingId);

        Task RemoveAsync(string userId, int listingId);

        Task<bool> IsBookmarkedAsync(string userId, int listingId);
    }
}
