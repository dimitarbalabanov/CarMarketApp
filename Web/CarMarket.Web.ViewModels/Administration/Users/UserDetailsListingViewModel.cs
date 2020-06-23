namespace CarMarket.Web.ViewModels.Administration.Users
{
    using System;

    public class UserDetailsListingViewModel
    {
        public int Id { get; set; }

        public string MakeName { get; set; }

        public string ModelName { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
