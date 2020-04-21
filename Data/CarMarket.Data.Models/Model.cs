namespace CarMarket.Data.Models
{
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Model : BaseModel<int>
    {
        public string Name { get; set; }

        public int MakeId { get; set; }

        public virtual Make Make { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}
