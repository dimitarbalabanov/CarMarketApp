namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ValidateFileTypeAndSizeAttribute : ValidationAttribute
    {
        private const string InvalidFileExtensionErrorMessage = "Image can be .jpg, .jpeg or .png format only.";
        private const string NoFileErrorMessage = "Image is required.";
        private const string InvalidFileSizeErrorMessage = "Image's size can be at most 1 MB.";

        private const int FileMaxSizeInBytes = 1048576;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var imageAsIFormFile = value as IFormFile;
            if (imageAsIFormFile == null)
            {
                return ValidationResult.Success;
            }

            var isValidExtension = this.ValidateFileExtension(imageAsIFormFile);
            if (!isValidExtension)
            {
                return new ValidationResult(InvalidFileExtensionErrorMessage);
            }

            var isValidSize = this.ValidateFileSize(imageAsIFormFile);
            if (!isValidSize)
            {
                return new ValidationResult(InvalidFileSizeErrorMessage);
            }

            return ValidationResult.Success;
        }

        private bool ValidateFileExtension(IFormFile file)
        {
            var extension = file.ContentType;
            return (extension != "image/jpeg" && extension != "image/png") ? false : true;
        }

        private bool ValidateFileSize(IFormFile file)
        {
            var size = file.Length;
            return (size > FileMaxSizeInBytes) ? false : true;
        }
    }
}
