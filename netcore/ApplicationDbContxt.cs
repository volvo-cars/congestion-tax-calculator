using congestion.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace congestion.calculator
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>().HasKey(x => x.Yare);

            modelBuilder.Entity<Calendar>()
            .Property(a => a.Holidays).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => string.IsNullOrEmpty(v) ? new List<DateTime>() : JsonConvert.DeserializeObject<List<DateTime>>(v));

            modelBuilder.Entity<Calendar>()
           .Property(a => a.Weekends).HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => string.IsNullOrEmpty(v) ? new List<DayOfWeek>() : JsonConvert.DeserializeObject<List<DayOfWeek>>(v));

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Calendar> Calendars { get; set; }
    }
}