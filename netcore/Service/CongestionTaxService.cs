using congestion.calculator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion.Service;

public class TollTaxRuleSet
{
    private List<TollTaxAmountRule> _tollTaxAmountRules;

    public TollTaxRuleSet()
    {
    }

    public int GetFee(DateTime passingDate)
    {
        var tollFee = _tollTaxAmountRules.Single(x => x.IsMach(new TimeOnly(passingDate.Hour, passingDate.Minute, passingDate.Second)));
        return tollFee.Amount;
    }

    private void FillRulles()
    {
        _tollTaxAmountRules = new();
    }
}