using System;
using System.Collections.Generic;
using Volvo.CongestionTax.Application.Commands;

namespace Volvo.CongestionTax.WebAPI.IntegrationTests
{
    public class InvalidCommands
    {
        public static IEnumerable<object[]> CalculateCongestionTaxCommands =>
            new List<object[]>
            {
                new object[]
                {
                    new CalculateCongestionTaxCommand
                    {
                        CountryCode = "",
                        City = "Gothenburg",
                        VehicleType = "Car",
                        PassagesTimes = new List<DateTime>
                        {
                            DateTime.Today
                        }
                    }
                },
                new object[]
                {
                    new CalculateCongestionTaxCommand
                    {
                        CountryCode = "SE",
                        City = "",
                        VehicleType = "Car",
                        PassagesTimes = new List<DateTime>
                        {
                            DateTime.Today
                        }
                    }
                },
                new object[]
                {
                    new CalculateCongestionTaxCommand
                    {
                        CountryCode = "SE",
                        City = "Gothenburg",
                        VehicleType = "",
                        PassagesTimes = new List<DateTime>
                        {
                            DateTime.Today
                        }
                    }
                },
                new object[]
                {
                    new CalculateCongestionTaxCommand
                    {
                        CountryCode = "SE",
                        City = "Gothenburg",
                        VehicleType = "Car",
                        PassagesTimes = new List<DateTime> { }
                    }
                },
            };
    }
}