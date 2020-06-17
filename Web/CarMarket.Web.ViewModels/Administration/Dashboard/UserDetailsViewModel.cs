namespace CarMarket.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;

    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<UserDetailsListingViewModel> Listings { get; set; }
    }
}
