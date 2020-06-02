namespace CarMarket.Web.ViewModels.Listings
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateListingInputModel
    {
        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public int BodyId { get; set; }

        public int TransmissionId { get; set; }

        public int FuelId { get; set; }

        public int ConditionId { get; set; }

        public int ColorId { get; set; }

        public int ProductionYear { get; set; }

        public int Mileage { get; set; }

        public int Horsepower { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IEnumerable<IFormFile> UploadImages { get; set; }
    }
}
