namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder
                .Property(f => f.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Fuel)
                .HasForeignKey(l => l.FuelId);
        }
    }
}
