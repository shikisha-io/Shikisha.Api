using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shikisha.DataAccess.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Shikisha.DataAccess
{
    public class ShikishaDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ShikishaDataContext(DbContextOptions<ShikishaDataContext> options) : base(options) { }

        private void SetDefaultEntityValues()
        {
            foreach (var insertedOrUpdatedEntry in ChangeTracker
                .Entries()
                .Where(entry => entry.Entity is EntityBase &&
                    (
                        entry.State == EntityState.Added
                        || entry.State == EntityState.Modified
                    )
                ))
            {
                // All insert/updates will update the updated timestamp
                ((EntityBase)insertedOrUpdatedEntry.Entity).UpdatedUtc = DateTime.UtcNow;

                // Inserted timestamp will be created when an entity is added
                if (insertedOrUpdatedEntry.State == EntityState.Added)
                {
                    ((EntityBase)insertedOrUpdatedEntry.Entity).Id = Guid.NewGuid();
                    ((EntityBase)insertedOrUpdatedEntry.Entity).InsertedUtc = DateTime.UtcNow;
                }
            }
        }

        public override int SaveChanges()
        {
            this.SetDefaultEntityValues();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetDefaultEntityValues();
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(product =>
            {
                product.ToTable("Products")
                    .HasKey(key => key.Id);

                product.HasMany(p => p.Projects)
                    .WithOne(project => project.Product)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}