namespace CarMarket.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
       Task<string> UploadImageAsync(IFormFile formFile, string name);
    }
}
