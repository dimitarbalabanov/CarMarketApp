namespace CarMarket.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CarMarket.Data.Common.Models;

    public class Listing : BaseModel<int>
    {
        public string SellerId { get; set; }

        public virtual ApplicationUser Seller { get; set; }

        public int DescriptionId { get; set; }

        public Description Description { get; set; }

        public double Mileage { get; set; }

        public double Horsepower { get; set; }

        public DateTime ProductionYear { get; set; }

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
