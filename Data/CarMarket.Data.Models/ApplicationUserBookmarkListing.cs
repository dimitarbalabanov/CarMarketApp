namespace CarMarket.Data.Models
{
    using System;

    using CarMarket.Data.Common.Models;

    public class ApplicationUserBookmarkListing : ICreatedOn
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ListingId { get; set; }

        public virtual Listing Listing { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
