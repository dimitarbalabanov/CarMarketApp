namespace CarMarket.Data.Models
{
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Color : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}
