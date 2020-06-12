namespace CarMarket.Services.Cloudinary
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile, string name);

        Task DestroyImageAsync(string publicId);
    }
}
