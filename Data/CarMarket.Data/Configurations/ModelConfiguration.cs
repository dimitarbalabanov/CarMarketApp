namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
               .HasMany(b => b.Listings)
               .WithOne(l => l.Model)
               .HasForeignKey(l => l.ModelId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
