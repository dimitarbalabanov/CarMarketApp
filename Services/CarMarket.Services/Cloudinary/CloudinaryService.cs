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
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile formFile, string name)
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
                AllowedFormats = new string[] { "jpg", "png" },
                // Transformation = new Transformation(),
            };

            var uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            stream.Dispose();

            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
