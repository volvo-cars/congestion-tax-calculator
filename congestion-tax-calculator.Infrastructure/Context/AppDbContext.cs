using congestion_tax_calculator.Application.Interfaces.Context;
using congestion_tax_calculator.Domain.Common;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Domain.Entities.Tax;
using congestion_tax_calculator.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace congestion_tax_calculator.Infrastructure.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<CongestionTaxRule> CongestionTaxRules { get; set; }
        public DbSet<Passage> Passages { get; set; }
        public DbSet<TaxExemptionRule> TaxExemptionRules { get; set; }
        public DbSet<TollFreeSpecificDate> TollFreeSpecificDates { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CongestionTaxRuleConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PassageConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TaxExemptionRuleConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TollFreeSpecificDateConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CountryConfig());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CurrencyConfig());
            base.OnModelCreating(modelBuilder);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedTime = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.InsertTime = DateTime.Now;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
