using congestion_tax_calculator.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsoCode).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Culture).HasMaxLength(8).IsRequired();
            builder.Property(x => x.CurrencyId);
            builder.HasIndex(e => new { e.IsoCode }, "country_code_unique")
                    .IsUnique();
            builder.HasOne(x => x.Currency).WithMany().HasForeignKey(x => x.CurrencyId);
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
    
}
