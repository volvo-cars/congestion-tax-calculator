using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Volvo.CongestionTax.Domain.Entities;
using Volvo.CongestionTax.Domain.ValueObjects;

namespace Volvo.CongestionTax.Infrastructure.EFCore.Configurations
{
    public class CityCongestionTaxRulesConfiguration : IEntityTypeConfiguration<CityCongestionTaxRules>
    {
        public void Configure(EntityTypeBuilder<CityCongestionTaxRules> builder)
        {
            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasMany(o => o.TaxExemptVehicles)
                .WithMany(x => x.CityCongestionTaxRulesList)
                .UsingEntity(j => j.ToTable("CityCongestionTaxRulesTaxExemptVehicles"));

            builder.Navigation(x => x.TaxExemptVehicles).AutoInclude();

            builder.Property(c => c.TollFreeDates).HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<ICollection<DateTime>>(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})).IsRequired();

            builder.Property(c => c.TimeZoneAmounts).HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}),
                v => JsonConvert.DeserializeObject<ICollection<TimeZoneAmount>>(v,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})).IsRequired();

            builder.Ignore(c => c.DomainEvents);
        }
    }
}