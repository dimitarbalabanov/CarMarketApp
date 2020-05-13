using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Listings
{
    public class EditListingInputModel
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

        public IEnumerable<SelectListItem> Models { get; set; }

        public IEnumerable<SelectListItem> Bodies { get; set; }

        public IEnumerable<SelectListItem> Transmissions { get; set; }

        public IEnumerable<SelectListItem> Fuels { get; set; }

        public IEnumerable<SelectListItem> Conditions { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }
    }
}
