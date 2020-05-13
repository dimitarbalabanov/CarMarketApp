namespace CarMarket.Web.ViewModels.Listings
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IEnumerable<SelectListItem> Makes { get; set; }

        public IEnumerable<SelectListItem> Bodies { get; set; }

        public IEnumerable<SelectListItem> Transmissions { get; set; }

        public IEnumerable<SelectListItem> Fuels { get; set; }

        public IEnumerable<SelectListItem> Conditions { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        // Make  - select list - id , string
        // Model - select list - id , string
        // Body - select list - id , string
        // Transmission - select list - id , string
        // Fuel - select list - id , string
        // Condition - select list - id , string
        // Color - select list - id , string
        // Prod Year - int
        // Mileage - int
        // Horsepower - int
        // Price - decimal
        // Description - string
        // Images - iformfile
    }
}
