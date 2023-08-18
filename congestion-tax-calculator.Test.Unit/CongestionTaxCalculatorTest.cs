using congestion.calculator;
using congestion.Contract;
using congestion.Model;
using congestion.Service;
using Moq;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Metadata;

namespace congestion_tax_calculator.Test.Unit
{
    public class CongestionTaxCalculatorTest : IClassFixture<TaxCalculatorTestFixture>
    {
        private TaxCalculatorTestFixture _testFixture;
        private ICalendarRepository _calendarRepository;
        private Mock<ICalendarRepository> _calendarRepositoryMock;
        private TollTaxRuleSetBuilder _tollTaxRuleSetBuilder;

        public CongestionTaxCalculatorTest(TaxCalculatorTestFixture fixture)
        {
            _testFixture = fixture;
            _tollTaxRuleSetBuilder = new TollTaxRuleSetBuilder();
            _calendarRepositoryMock = new Mock<ICalendarRepository>();
            _calendarRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(fixture.Calendar);

            _calendarRepository = _calendarRepositoryMock.Object;
        }

        [Fact]
        public void Calc_TollFee_For_SingleRule()
        {
            var car = new Car();
            var firstPassingDate = new DateTime(2013, 1, 2, 6, 5, 0);
            DateTime[] dateTimes = new[] { firstPassingDate, firstPassingDate.AddMinutes(10), firstPassingDate.AddMinutes(40) };

            var sut = new CongestionSingleCHargeTaxCalculator(_calendarRepository, _testFixture.TollTaxRuleSet);
            int expectedFee = 13;
            var tollFee = sut.GetTax(car, dateTimes);

            Assert.Equal(expectedFee, tollFee);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetVehiclePassingData), MemberType = typeof(TestDataGenerator))]
        public void Calc_TollFee_For_GeneralRule(DateTime[] passingTimes, int expectedFee)
        {
            var car = new Car();
            var sut = new CongestionSingleCHargeTaxCalculator(_calendarRepository, _testFixture.TollTaxRuleSet);

            var tollFee = sut.GetTax(car, passingTimes);

            Assert.Equal(expectedFee, tollFee);
        }

        [Fact]
        public void Calc_Free_TollFee_For_July()
        {
            var car = new Car();
            var firstPassingDateInJuly = new DateTime(2013, 7, 2, 6, 5, 0);

            DateTime[] dateTimes = new[] { firstPassingDateInJuly, firstPassingDateInJuly.AddMinutes(10), firstPassingDateInJuly.AddMinutes(40) };
            var sut = new CongestionSingleCHargeTaxCalculator(_calendarRepository, _testFixture.TollTaxRuleSet);
            int expectedFee = 0;
            var tollFee = sut.GetTax(car, dateTimes);

            Assert.Equal(expectedFee, tollFee);
        }

        [Fact]
        public void Calc_Free_TollFee_For_Day_Befor_Holiday()
        {
            var holiday = new DateTime(2013, 5, 7, 8, 15, 0);
            CalendarBuilder tollCalendarBuilder = new();
            tollCalendarBuilder.ForYare(2013)
                .WithHolidayDates(new[] { holiday });
            _calendarRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(tollCalendarBuilder.Build());
            var car = new Car();
            var dateBeforHoliday = new DateTime(2013, 5, 6, 8, 15, 20);
            var sut = new CongestionSingleCHargeTaxCalculator(_calendarRepository, _testFixture.TollTaxRuleSet);
            int expectedFee = 0;
            var tollFee = sut.GetTax(car, new[] { dateBeforHoliday });

            Assert.Equal(expectedFee, tollFee);
        }

        [Fact]
        public void Calc_Free_TollFee_For_Holiday()
        {
            var holiday = new DateTime(2013, 5, 5, 8, 15, 0);
            CalendarBuilder tollCalendarBuilder = new();
            tollCalendarBuilder.ForYare(2013)
                .WithHolidayDates(new[] { holiday });
            _calendarRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(tollCalendarBuilder.Build());

            var car = new Car();
            var sut = new CongestionSingleCHargeTaxCalculator(_calendarRepository, _testFixture.TollTaxRuleSet);
            int expectedFee = 0;
            var tollFee = sut.GetTax(car, new[] { holiday });

            Assert.Equal(expectedFee, tollFee);
        }
    }
}

public class TestDataGenerator : IEnumerable<object[]>
{
    public static IEnumerable<object[]> GetVehiclePassingData()
    {
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-01-14 21:00:00") }, 0 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-01-15 21:00:00") }, 0 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-02-07 06:23:27"), DateTime.Parse("2013-02-07 15:27:00") }, 21 };

        yield return new object[] {
            new DateTime[] {
                DateTime.Parse("2013-02-08 06:27:00"),
                DateTime.Parse("2013-02-08 06:20:27") }
            , 8 };

        yield return new object[] {
            new DateTime[] {
                DateTime.Parse("2013-02-08 14:35:00"),
                DateTime.Parse("2013-02-08 15:29:00") ,
                DateTime.Parse("2013-02-08 15:47:00"),
                DateTime.Parse("2013-02-08 16:01:00"),
                DateTime.Parse("2013-02-08 16:48:00")
            }, 60 };

        yield return new object[] {
            new DateTime[] {
                DateTime.Parse("2013-02-08 14:35:00"),
                DateTime.Parse("2013-02-08 15:29:00") ,
                DateTime.Parse("2013-02-08 15:47:00"),
                DateTime.Parse("2013-02-08 16:01:00"),
            }, 56 };

        yield return new object[] { new DateTime[] { DateTime.Parse("2013-02-08 17:49:00") }, 13 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-02-08 18:29:00") }, 8 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-02-08 18:35:00") }, 0 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-03-26 14:25:00") }, 8 };
        yield return new object[] { new DateTime[] { DateTime.Parse("2013-03-28 14:07:27") }, 8 };
    }

    public IEnumerator<object[]> GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}