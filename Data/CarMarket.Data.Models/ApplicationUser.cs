namespace CarMarket.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, ICreatedOn
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }

        public virtual ICollection<ApplicationUserBookmarkListing> BookmarkListings { get; set; }
    }
}
