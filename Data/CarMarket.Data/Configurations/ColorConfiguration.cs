namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Color)
                .HasForeignKey(l => l.ColorId);
        }
    }
}
