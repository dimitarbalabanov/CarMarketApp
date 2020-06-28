namespace CarMarket.Web.ViewModels.Images
{
    using CarMarket.Web.Infrastructure.CustomValidation;

    using Microsoft.AspNetCore.Http;

    public class EditListingImageInputModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        [ValidateFileTypeAndSize]
        public IFormFile ImageFile { get; set; }

        public bool IsMain { get; set; }
    }
}
