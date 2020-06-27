namespace CarMarket.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using CarMarket.Data.Models;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> UploadAsync(IFormFile file, bool isMain = false);

        Task DeleteAllImagesByListingIdAsync(int listingId);

        Task ChangeImageByIdAsync(int id, IFormFile file, bool isMain);
    }
}
