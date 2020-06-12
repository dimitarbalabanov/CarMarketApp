namespace CarMarket.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> UploadAsync(IFormFile file, bool isMain);

        Task DeleteAllImagesByListingIdAsync(int listingId);
    }
}
