namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> user)
        {
            user
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            user
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            user
                .HasMany(u => u.Listings)
                .WithOne(l => l.Seller)
                .HasForeignKey(l => l.SellerId);
        }
    }
}
