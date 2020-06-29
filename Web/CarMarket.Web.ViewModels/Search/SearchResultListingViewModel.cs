namespace CarMarket.Web.ViewModels.Search
{
    using System;

    public class SearchResultListingViewModel
    {
        public int Id { get; set; }

        public string SellerId { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductionYear { get; set; }

        public string ModelName { get; set; }

        public string MakeName { get; set; }

        public string MakeModelName => $"{this.MakeName} {this.ModelName}";

        public string ImageUrl { get; set; }
    }
}
