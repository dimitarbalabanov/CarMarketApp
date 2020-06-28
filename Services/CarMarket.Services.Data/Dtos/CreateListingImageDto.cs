namespace CarMarket.Services.Data.Dtos
{
    using Microsoft.AspNetCore.Http;

    public class CreateListingImageDto
    {
        public IFormFile ImageFile { get; set; }

        public bool IsMain { get; set; }
    }
}
