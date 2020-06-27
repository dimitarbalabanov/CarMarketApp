namespace CarMarket.Services.Data.Dtos
{
    using Microsoft.AspNetCore.Http;

    public class EditImageDto
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; }

        public bool IsMain { get; set; }
    }
}
