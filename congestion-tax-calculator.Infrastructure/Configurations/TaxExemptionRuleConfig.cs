using congestion_tax_calculator.Domain.Entities.Tax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    internal class TaxExemptionRuleConfig : IEntityTypeConfiguration<TaxExemptionRule>
    {
        public void Configure(EntityTypeBuilder<TaxExemptionRule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.VehicleType).IsRequired();
            builder.Property(x => x.TollFreeVehicles).IsRequired();
            builder.Property(x => x.IsExempt).IsRequired();
            builder.HasIndex(e => new { e.CityId, e.VehicleType, e.TollFreeVehicles }, "taxexemptionrule_cityid_vehicletype_tollfreevehicle_unique")
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
