using congestion_tax_calculator.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    public class TollFreeSpecificDateConfig : IEntityTypeConfiguration<TollFreeSpecificDate>
    {
        public void Configure(EntityTypeBuilder<TollFreeSpecificDate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
