﻿namespace CarMarket.Data.Models
{
    using CarMarket.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        public string ImageUrl { get; set; }

        public string PublicId { get; set; }

        public bool IsMain { get; set; }

        public int ListingId { get; set; }

        public virtual Listing Listing { get; set; }
    }
}
