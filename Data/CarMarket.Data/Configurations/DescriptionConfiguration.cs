namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DescriptionConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder
                .Property(d => d.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Description)
                .HasForeignKey(l => l.DescriptionId);
        }
    }
}
