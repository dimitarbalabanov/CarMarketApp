namespace CarMarket.Data.Models
{
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Make : BaseModel<int>
    {
        public Make()
        {
            this.Models = new HashSet<Model>();
        }

        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
