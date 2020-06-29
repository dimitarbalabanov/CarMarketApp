namespace CarMarket.Web.ViewModels.Home
{
    public class HomeListingViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public int ProductionYear { get; set; }

        public string ModelName { get; set; }

        public string MakeName { get; set; }

        public string MakeModelName => $"{this.MakeName} {this.ModelName}";

        public string ImageUrl { get; set; }
    }
}
