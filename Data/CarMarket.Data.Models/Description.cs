namespace CarMarket.Data.Models
{
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Description : BaseModel<int>
    {
        public string Content { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}
