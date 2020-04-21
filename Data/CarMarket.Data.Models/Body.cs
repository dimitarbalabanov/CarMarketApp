namespace CarMarket.Data.Models
{
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Body : BaseModel<int>
    {
        public string Type { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}
