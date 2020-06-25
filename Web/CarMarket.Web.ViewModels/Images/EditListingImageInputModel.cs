namespace CarMarket.Web.ViewModels.Images
{
    using Microsoft.AspNetCore.Http;

    public class EditListingImageInputModel
    {
        public string PublicId { get; set; }

        public IFormFile Image { get; set; }
    }
}
