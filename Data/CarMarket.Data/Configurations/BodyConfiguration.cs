namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BodyConfiguration : IEntityTypeConfiguration<Body>
    {
        public void Configure(EntityTypeBuilder<Body> builder)
        {
            builder
                .Property(b => b.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Body)
                .HasForeignKey(l => l.BodyId);
        }
    }
}
