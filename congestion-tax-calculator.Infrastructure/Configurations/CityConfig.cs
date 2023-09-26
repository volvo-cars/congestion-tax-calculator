using congestion_tax_calculator.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.HasPublicHolidaysExemp).IsRequired();
            builder.Property(x => x.HasWeekendsExemp).IsRequired();
            builder.Property(x => x.CongestionTaxRules);
            builder.Property(x => x.TaxExemptionRules);
            builder.Property(x => x.TollFreeSpecificDates); 
            builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
            builder.HasIndex(e => new { e.Name}, "city_name_unique")
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
