using congestion.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace congestion.calculator.EfMapping
{
    internal class CalendarMapping : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.HasKey(p => p.Yare);

            builder
            .Property(p => p.Holidays).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => string.IsNullOrEmpty(v) ? new List<DateTime>() : JsonConvert.DeserializeObject<List<DateTime>>(v));

            builder
           .Property(p => p.Weekends).HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => string.IsNullOrEmpty(v) ? new List<DayOfWeek>() : JsonConvert.DeserializeObject<List<DayOfWeek>>(v));
        }
    }
}