using congestion_tax_calculator.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Symbol).IsRequired();
            builder.Property(x => x.Rate);
            builder.HasIndex(e => new { e.Code }, "currency_code_unique")
                    .IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
