namespace CarMarket.Data.Configurations
{
    using CarMarket.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserBookmarkListingConfiguration : IEntityTypeConfiguration<ApplicationUserBookmarkListing>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserBookmarkListing> builder)
        {
            builder
                .HasKey(ul => new { ul.UserId, ul.ListingId });

            builder
                .HasOne(ul => ul.Listing)
                .WithMany(u => u.UserBookmarkings)
                .HasForeignKey(b => b.ListingId);

            builder
                .HasOne(ul => ul.User)
                .WithMany(u => u.BookmarkListings)
                .HasForeignKey(bl => bl.UserId);
        }
    }
}
