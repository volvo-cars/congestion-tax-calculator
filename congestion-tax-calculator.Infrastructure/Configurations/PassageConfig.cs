using congestion_tax_calculator.Domain.Entities.Tax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    internal class PassageConfig : IEntityTypeConfiguration<Passage>
    {
        public void Configure(EntityTypeBuilder<Passage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CityId).IsRequired();
            builder.Property(x => x.PassageNumberPlates).IsRequired();
            builder.Property(x => x.PassageTime).IsRequired();
            builder.Property(x => x.IsTaxExempt).IsRequired(); 
            builder.Property(x => x.VehicleType).IsRequired();
            builder.Property(x => x.TollFreeVehicles).IsRequired();
            builder.HasIndex(e => new { e.CityId, e.PassageNumberPlates, e.PassageTime }, "passage_cityid_numberplates_time_unique")
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
