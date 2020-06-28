namespace CarMarket.Services.Data.Dtos
{
    using Microsoft.AspNetCore.Http;

    public class CreateListingInputImageDto
    {
        public IFormFile ImageFile { get; set; }

        public bool IsMain { get; set; }
    }
}
