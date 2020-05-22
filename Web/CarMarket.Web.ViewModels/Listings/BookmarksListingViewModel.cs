using System;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Listings
{
    public class BookmarksListingViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductionYear { get; set; }

        public string ModelName { get; set; }

        public string MakeName { get; set; }

        public string ImageUrl { get; set; }
    }
}
