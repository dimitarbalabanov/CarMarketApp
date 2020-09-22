namespace CarMarket.Services.Cloudinary
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private const string JpgFormat = "jpg";
        private const string PngFormat = "png";

        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<UploadResultDto> UploadImageAsync(IFormFile formFile, string name)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            byte[] image;
            var memoryStream = new MemoryStream();

            await formFile.CopyToAsync(memoryStream);
            image = memoryStream.ToArray();
            memoryStream.Dispose();

            var stream = new MemoryStream(image);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream),
                AllowedFormats = new string[] { JpgFormat, PngFormat },
            };

            var uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            stream.Dispose();

            var result = new UploadResultDto
            {
                AbsoluteUri = uploadResult.SecureUri.AbsoluteUri,
                PublicId = uploadResult.PublicId,
            };

            return result;
        }

        public async Task DestroyImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            await this.cloudinary.DestroyAsync(deletionParams);
        }
    }
}
