namespace CarMarket.Services.Data.Dtos
{
    public class SearchInfoModelDto
    {
        public string MakeName { get; set; }

        public string ModelName { get; set; }

        public string BodyType { get; set; }

        public string TransmissionType { get; set; }

        public string FuelType { get; set; }

        public string ConditionType { get; set; }

        public string ColorName { get; set; }

        public int? ProductionYearFrom { get; set; }

        public int? ProductionYearTo { get; set; }

        public int? MileageFrom { get; set; }

        public int? MileageTo { get; set; }

        public int? HorsepowerFrom { get; set; }

        public int? HorsepowerTo { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public string OrderingValueString { get; set; }
    }
}
