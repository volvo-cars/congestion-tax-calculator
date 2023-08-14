using System;
using System.Collections.Generic;

namespace congestion.calculator
{
    public static class DB
    {
        public static List<TollTaxAmountRule> GetTollFeeAmounts()
        {
            //TODO:Fill from json file for other city
            List<TollTaxAmountRule> result = new();
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(6, 0), To = new TimeOnly(6, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(6, 30), To = new TimeOnly(6, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 18, From = new TimeOnly(7, 0), To = new TimeOnly(7, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(8, 0), To = new TimeOnly(8, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(8, 30), To = new TimeOnly(14, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(15, 0), To = new TimeOnly(15, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 18, From = new TimeOnly(15, 30), To = new TimeOnly(16, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(17, 0), To = new TimeOnly(17, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(18, 0), To = new TimeOnly(18, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 0, From = new TimeOnly(18, 30), To = new TimeOnly(5, 59, 59) });
            return result;
        }

        public static List<TollTaxAmountRule> AddVehiclePassing(Vehicle vehicle, DateTime aTime)
        {
            //TODO:Fill from json file for other city
            List<TollTaxAmountRule> result = new();
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(6, 0), To = new TimeOnly(6, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(6, 30), To = new TimeOnly(6, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 18, From = new TimeOnly(7, 0), To = new TimeOnly(7, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(8, 0), To = new TimeOnly(8, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(8, 30), To = new TimeOnly(14, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(15, 0), To = new TimeOnly(15, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 18, From = new TimeOnly(15, 30), To = new TimeOnly(16, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 13, From = new TimeOnly(17, 0), To = new TimeOnly(17, 59, 59) });
            result.Add(new TollTaxAmountRule { Amount = 8, From = new TimeOnly(18, 0), To = new TimeOnly(18, 29, 59) });
            result.Add(new TollTaxAmountRule { Amount = 0, From = new TimeOnly(18, 30), To = new TimeOnly(5, 59, 59) });
            return result;
        }
    }
}