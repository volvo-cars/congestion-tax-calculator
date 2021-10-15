using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Volvo.CongestionTax.Application.Commands;
using Volvo.CongestionTax.WebAPI.IntegrationTests.Extensions;
using Xunit;

namespace Volvo.CongestionTax.WebAPI.IntegrationTests
{
    public class TaxControllerTests : TestBase
    {
        private const string CalculateCongestionTaxUri = "/api/tax/calculate";

        [Theory]
        [InlineData(2013, 6, 6, 6, 15, 8)]
        [InlineData(2013, 6, 6, 6, 35, 13)]
        [InlineData(2013, 6, 6, 7, 15, 18)]
        [InlineData(2013, 6, 6, 8, 12, 13)]
        [InlineData(2013, 6, 6, 12, 00, 8)]
        [InlineData(2013, 6, 6, 15, 15, 13)]
        [InlineData(2013, 6, 6, 16, 10, 18)]
        [InlineData(2013, 6, 6, 17, 30, 13)]
        [InlineData(2013, 6, 6, 18, 21, 8)]
        [InlineData(2013, 6, 6, 19, 30, 0)]
        public async Task ShouldCalculateReturnExpectedAmountForGivenTimeSchedule(int year, int month, int day,
            int hour, int minute, decimal amount)
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                "Car",
                new List<DateTime>
                {
                    new(year, month, day, hour, minute, 00)
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(amount);
        }

        [Theory]
        [InlineData(2013, 1, 1, 8, 15)]
        [InlineData(2013, 5, 1, 12, 15)]
        [InlineData(2013, 12, 25, 9, 15)]
        public async Task ShouldCalculateReturnZeroForPublicHolidays(int year, int month, int day,
            int hour, int minute)
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                "Car",
                new List<DateTime>
                {
                    new(year, month, day, hour, minute, 00),
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(0);
        }

        [Theory]
        [InlineData("Bus")]
        [InlineData("Emergency")]
        [InlineData("Diplomat")]
        [InlineData("Motorcycle")]
        [InlineData("Military")]
        [InlineData("Foreign")]
        public async Task ShouldCalculateReturnZeroForTaxExemptVehicles(string taxExemptVehicle)
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                taxExemptVehicle,
                new List<DateTime>
                {
                    new(2013, 1, 10, 8, 45, 00),
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(0);
        }

        [Fact]
        public async Task ShouldCalculateReturnZeroForJuly()
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                "Car",
                new List<DateTime>
                {
                    new(2013, 7, 1, 8, 00, 00),
                    new(2013, 7, 2, 9, 00, 00),
                    new(2013, 7, 3, 10, 00, 00),
                    new(2013, 7, 4, 11, 00, 00),
                    new(2013, 7, 5, 12, 00, 00),
                    new(2013, 7, 6, 13, 00, 00),
                    new(2013, 7, 7, 14, 00, 00),
                    new(2013, 7, 8, 15, 00, 00),
                    new(2013, 7, 9, 16, 00, 00),
                    new(2013, 7, 10, 17, 00, 00),
                    new(2013, 7, 11, 18, 00, 00),
                    new(2013, 7, 12, 19, 00, 00),
                    new(2013, 7, 13, 20, 00, 00),
                    new(2013, 7, 14, 21, 00, 00),
                    new(2013, 7, 15, 22, 00, 00),
                    new(2013, 7, 16, 23, 00, 00),
                    new(2013, 7, 17, 23, 59, 00),
                    new(2013, 7, 18, 1, 00, 00),
                    new(2013, 7, 19, 2, 00, 00),
                    new(2013, 7, 20, 3, 00, 00),
                    new(2013, 7, 21, 4, 00, 00),
                    new(2013, 7, 22, 5, 00, 00),
                    new(2013, 7, 23, 6, 00, 00),
                    new(2013, 7, 24, 7, 00, 00),
                    new(2013, 7, 25, 8, 00, 00),
                    new(2013, 7, 26, 9, 00, 00),
                    new(2013, 7, 27, 10, 00, 00),
                    new(2013, 7, 28, 11, 00, 00),
                    new(2013, 7, 29, 12, 00, 00),
                    new(2013, 7, 30, 13, 00, 00),
                    new(2013, 7, 31, 14, 00, 00)
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(0);
        }


        [Fact]
        public async Task ShouldCalculateReturnMaxTollAmountHappenedIn60Min()
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                "Car",
                new List<DateTime>
                {
                    new(2013, 6, 10, 6, 20, 00), //SEK 8
                    new(2013, 6, 10, 7, 20, 00), //SEK 18
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(18);
        }

        [Fact]
        public async Task ShouldCalculateReturn21ForGivenPassages()
        {
            var response = await SendCalculateCongestionTaxRequest("SE",
                "Gothenburg",
                "Car",
                new List<DateTime>
                {
                    new(2013, 6, 10, 6, 20, 00), //SEK 8
                    new(2013, 6, 10, 8, 20, 00), //SEK 13
                    new(2013, 6, 10, 19, 50, 00), //SEK 0
                    new(2013, 6, 10, 1, 40, 00), //SEK 0
                });

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.DeserializeAsAsync<CalculateCongestionTaxCommandResult>();
            result.Amount.Should().Be(21);
        }

        private async Task<HttpResponseMessage> SendCalculateCongestionTaxRequest(string countryCode,
            string city,
            string vehicleType,
            IList<DateTime> passagesTimes)
        {
            var calculateCongestionTaxCommand = new CalculateCongestionTaxCommand
            {
                CountryCode = countryCode,
                City = city,
                VehicleType = vehicleType,
                PassagesTimes = passagesTimes
            };

            HttpContent httpContent =
                new StringContent(JsonConvert.SerializeObject(calculateCongestionTaxCommand), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var createOrderHttpResponseMessage =
                await Client.PostAsync(CalculateCongestionTaxUri, httpContent, default);

            return createOrderHttpResponseMessage;
        }
    }
}