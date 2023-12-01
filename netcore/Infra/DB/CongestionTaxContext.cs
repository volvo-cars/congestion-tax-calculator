using congestion.Entity;
using congestion.Infra.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.Infra.DB
{
    public class CongestionTaxContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<TaxRule> TaxRules { get; set; }
        public DbSet<ExemptVehicle> ExemptVehicles { get; set; }
        public DbSet<ExemptDay> ExemptDays { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBSettings.ConnectionString);
        }
    }
}
