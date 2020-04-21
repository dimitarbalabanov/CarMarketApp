namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConditionConfiguration : IEntityTypeConfiguration<Condition>
    {
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder
                .Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Listings)
                .WithOne(l => l.Condition)
                .HasForeignKey(l => l.ConditionId);
        }
    }
}
