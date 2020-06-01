namespace CarMarket.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SearchInputModel
    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public int? BodyId { get; set; }

        public int? TransmissionId { get; set; }

        public int? FuelId { get; set; }

        public int? ConditionId { get; set; }

        public int? ColorId { get; set; }

        public int? ProductionYearFrom { get; set; }

        public int? ProductionYearTo { get; set; }

        public int? MileageFrom { get; set; }

        public int? MileageTo { get; set; }

        public int? HorsepowerFrom { get; set; }

        public int? HorsepowerTo { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public int OrderingValue { get; set; }

        public IEnumerable<SelectListItem> Makes { get; set; }

        public IEnumerable<SelectListItem> Bodies { get; set; }

        public IEnumerable<SelectListItem> Transmissions { get; set; }

        public IEnumerable<SelectListItem> Fuels { get; set; }

        public IEnumerable<SelectListItem> Conditions { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        public IEnumerable<SelectListItem> OrderingValues { get; set; }
    }
}
