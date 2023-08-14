using congestion.calculator;
using congestion.Model;
using congestion.Service;
using System.Collections.ObjectModel;

namespace congestion_tax_calculator.Test.Unit
{
    public class CongestionTaxCalculatorTest
    {
        [Fact]
        public void Calc_TollFee_In_TollFree_Date()
        {
            var car = new Car();
            TollCalendarBuilder tollCalendarBuilder = new();
            var sut = new CongestionTaxCalculator(tollCalendarBuilder.Build(), new TollFeeService());

            var tollFreeVehiclePassingDate = new DateTime(2013, 1, 5);
            var tollFee = sut.GetTollFee(tollFreeVehiclePassingDate, car);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void Calc_TollFee_For_TollFree_Vehicle()
        {
            var car = new Emergency();
            TollCalendarBuilder tollCalendarBuilder = new();
            var sut = new CongestionTaxCalculator(tollCalendarBuilder.Build(), new TollFeeService());

            var passingDate = new DateTime(2013, 1, 2, 8, 17, 0);
            var tollFee = sut.GetTollFee(passingDate, car);

            Assert.Equal(0, tollFee);
        }

        [Theory]
        [InlineData("8:29", 13)]
        [InlineData("7:29", 18)]
        [InlineData("10:15", 8)]
        [InlineData("20:15", 0)]
        public void Calc_TollFee_For_Vehicle(string time, decimal expectedFee)
        {
            var car = new Car();
            TollCalendarBuilder tollCalendarBuilder = new();
            var sut = new CongestionTaxCalculator(tollCalendarBuilder.Build(), new TollFeeService());
            var hour = int.Parse(time.Split(':')[0]);
            var minute = int.Parse(time.Split(':')[1]);
            var passingDate = new DateTime(2013, 1, 2, hour, minute, 0);
            var tollFee = sut.GetTollFee(passingDate, car);

            Assert.Equal(expectedFee, tollFee);
        }

        [Fact]
        public void Calc_TollFee_For_SingleRule()
        {
            var car = new Car();
            var firstPassingDate = new DateTime(2013, 1, 2, 6, 5, 0);
            DateTime[] dateTimes = new[] { firstPassingDate, firstPassingDate.AddMinutes(10), firstPassingDate.AddMinutes(40) };
            TollCalendarBuilder tollCalendarBuilder = new();
            var sut = new CongestionTaxCalculator(tollCalendarBuilder.Build(), new TollTaxRuleSet());
            int expectedFee = 13;
            var tollFee = sut.GetTax(car, dateTimes);

            Assert.Equal(expectedFee, tollFee);
        }
    }
}