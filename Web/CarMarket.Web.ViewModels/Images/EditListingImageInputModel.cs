namespace CarMarket.Web.ViewModels.Images
{
    using Microsoft.AspNetCore.Http;

    public class EditListingImageInputModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
