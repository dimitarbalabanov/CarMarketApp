namespace CarMarket.Web.ViewModels.Images
{
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Web.Infrastructure.CustomValidation;

    using Microsoft.AspNetCore.Http;

    public class CreateListingImageInputModel
    {
        private const string ImageRequiredErrorMessage = "Please choose an image.";

        [Required(ErrorMessage = ImageRequiredErrorMessage)]
        [ValidateFileTypeAndSize]
        public IFormFile ImageFile { get; set; }

        public bool IsMain { get; set; }
    }
}
