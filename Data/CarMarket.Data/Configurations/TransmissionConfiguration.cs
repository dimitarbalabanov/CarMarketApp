namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
    {
        public void Configure(EntityTypeBuilder<Transmission> builder)
        {
            builder
                .Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Transmission)
                .HasForeignKey(l => l.TransmissionId);
        }
    }
}
