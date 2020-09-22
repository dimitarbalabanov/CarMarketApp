namespace CarMarket.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<UploadResultDto> UploadImageAsync(IFormFile formFile, string name);

        Task DestroyImageAsync(string publicId);
    }
}
