using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.CongestionTax.Domain.Entities;
using Volvo.CongestionTax.Domain.ValueObjects;
using Volvo.CongestionTax.Infrastructure.EFCore;
using Volvo.Domain.SharedKernel;
using TimeZone = Volvo.Domain.SharedKernel.TimeZone;

namespace Volvo.CongestionTax.WebAPI
{
    public class DbExampleSeeder
    {
        public static async Task SeedSampleDataAsync(CongestionTaxDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.CityCongestionTaxRules.Any())
            {
                await context.CityCongestionTaxRules.AddAsync(new CityCongestionTaxRules
                {
                    CountryCode = "SE",
                    City = "Gothenburg",
                    MaxDailyTollAmount = 60M,
                    TaxExemptVehicles = new List<Vehicle>
                    {
                        new()
                        {
                            Type = "Emergency"
                        },
                        new()
                        {
                            Type = "Bus"
                        },
                        new()
                        {
                            Type = "Diplomat"
                        },
                        new()
                        {
                            Type = "Motorcycle"
                        },
                        new()
                        {
                            Type = "Military"
                        },
                        new()
                        {
                            Type = "Foreign"
                        }
                    },
                    TollFreeDates = new List<DateTime>
                    {
                        new(2013, 7, 1),
                        new(2013, 7, 2),
                        new(2013, 7, 3),
                        new(2013, 7, 4),
                        new(2013, 7, 5),
                        new(2013, 7, 6),
                        new(2013, 7, 7),
                        new(2013, 7, 8),
                        new(2013, 7, 9),
                        new(2013, 7, 10),
                        new(2013, 7, 11),
                        new(2013, 7, 12),
                        new(2013, 7, 13),
                        new(2013, 7, 14),
                        new(2013, 7, 15),
                        new(2013, 7, 16),
                        new(2013, 7, 17),
                        new(2013, 7, 18),
                        new(2013, 7, 19),
                        new(2013, 7, 20),
                        new(2013, 7, 21),
                        new(2013, 7, 22),
                        new(2013, 7, 23),
                        new(2013, 7, 24),
                        new(2013, 7, 25),
                        new(2013, 7, 26),
                        new(2013, 7, 27),
                        new(2013, 7, 28),
                        new(2013, 7, 29),
                        new(2013, 7, 30),
                        new(2013, 7, 31)
                    },
                    TimeZoneAmounts = new List<TimeZoneAmount>
                    {
                        new()
                        {
                            TimeZone = new TimeZone(new Time(6, 00), new Time(6, 29)),
                            Amount = 8M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(6, 30), new Time(6, 59)),
                            Amount = 13M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(7, 00), new Time(7, 59)),
                            Amount = 18M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(8, 00), new Time(8, 29)),
                            Amount = 13M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(8, 30), new Time(14, 59)),
                            Amount = 8M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(15, 00), new Time(15, 29)),
                            Amount = 13M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(15, 30), new Time(16, 59)),
                            Amount = 18M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(17, 00), new Time(17, 59)),
                            Amount = 13M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(18, 00), new Time(18, 29)),
                            Amount = 8M
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(18, 30), new Time(23, 59)),
                            Amount = 0
                        },
                        new()
                        {
                            TimeZone = new TimeZone(new Time(00, 00), new Time(05, 59)),
                            Amount = 0
                        }
                    }
                });
            }

            if (!context.PublicHolidays.Any())
            {
                await context.PublicHolidays.AddAsync(new PublicHoliday()
                {
                    CountryCode = "SE",
                    Date = new DateTime(2013,1,1),
                    Name = "New Year's Day",
                });

                await context.PublicHolidays.AddAsync(new PublicHoliday()
                {
                    CountryCode = "SE",
                    Date = new DateTime(2013, 5, 1),
                    Name = "Labour's Day",
                });

                await context.PublicHolidays.AddAsync(new PublicHoliday()
                {
                    CountryCode = "SE",
                    Date = new DateTime(2013, 12, 25),
                    Name = "Christmas",
                });
            }

            await context.SaveChangesAsync();
        }
    }
}