using congestion_tax_calculator.Domain.Entities.Tax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    internal class CongestionTaxRuleConfig : IEntityTypeConfiguration<CongestionTaxRule>
    {
        public void Configure(EntityTypeBuilder<CongestionTaxRule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.StartHour).IsRequired();
            builder.Property(x => x.EndHour).IsRequired();
            builder.Property(x => x.TollFee).IsRequired();
            builder.Property(x => x.IsDisabled).IsRequired();
            builder.HasIndex(e => new { e.CityId, e.StartHour, e.EndHour }, "congestiontaxrule_cityid_starthour_endhour_unique")
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
