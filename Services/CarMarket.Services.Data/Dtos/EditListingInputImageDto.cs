namespace CarMarket.Services.Data.Dtos
{
    using Microsoft.AspNetCore.Http;

    public class EditListingInputImageDto
    {
        public int Id { get; set; }

        public IFormFile ImageFile { get; set; }

        public bool IsMain { get; set; }
    }
}
