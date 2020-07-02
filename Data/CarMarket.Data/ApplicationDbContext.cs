namespace CarMarket.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CarMarket.Data.Common.Models;
    using CarMarket.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Body> Bodies { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<Fuel> Fuels { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ApplicationUserBookmarkListing> ApplicationUsersBookmarkListings { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyCreatedOn();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyCreatedOn();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.ApplyEntityConfigurations(builder);

            // var entityTypes = builder.Model.GetEntityTypes().ToList();
            // var foreignKeys = entityTypes
            //     .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            // foreach (var foreignKey in foreignKeys)
            // {
            //    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            // }
        }

        private void ApplyEntityConfigurations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyCreatedOn()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is ICreatedOn && e.State == EntityState.Added);

            foreach (var entry in changedEntries)
            {
                var entity = (ICreatedOn)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
