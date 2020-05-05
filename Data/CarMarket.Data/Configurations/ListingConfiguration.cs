namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder
                .Property(l => l.Mileage)
                .IsRequired();

            builder
                .Property(l => l.Horsepower)
                .IsRequired();

            builder
                .Property(l => l.SeatsCount)
                .IsRequired();

            builder
                .Property(l => l.ProductionYear)
                .IsRequired();

            builder
                .Property(l => l.Price)
                .IsRequired();
        }
    }
}
