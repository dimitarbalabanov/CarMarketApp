namespace CarMarket.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
       Task<string> UploadImage(IFormFile fileForm, string name);
    }
}
