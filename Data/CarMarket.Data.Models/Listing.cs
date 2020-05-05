namespace CarMarket.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Listing : BaseModel<int>
    {
        public Listing()
        {
            this.Images = new HashSet<Image>();
            this.UserBookmarkings = new HashSet<ApplicationUserBookmarkListing>();
        }

        public string SellerId { get; set; }

        public virtual ApplicationUser Seller { get; set; }

        public int DescriptionId { get; set; }

        public Description Description { get; set; }

        public int Mileage { get; set; }

        public int Horsepower { get; set; }

        public int SeatsCount { get; set; }

        public int ProductionYear { get; set; }

        public int ConditionId { get; set; }

        public Condition Condition { get; set; }

        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int BodyId { get; set; }

        public virtual Body Body { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public int TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }

        public int FuelId { get; set; }

        public virtual Fuel Fuel { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ApplicationUserBookmarkListing> UserBookmarkings { get; set; }
    }
}
