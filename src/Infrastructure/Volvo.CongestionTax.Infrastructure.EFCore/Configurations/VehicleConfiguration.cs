using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volvo.CongestionTax.Domain.Entities;

namespace Volvo.CongestionTax.Infrastructure.EFCore.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(a => a.Type)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}