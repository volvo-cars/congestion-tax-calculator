using congestion.calculator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace congestion.Service;

public class TollTaxRuleSet
{
    private List<TollTaxRule> _tollTaxRules;

    public static TollTaxRuleSet Create(string tollTaxRulesPath)
    {
        if (File.Exists(tollTaxRulesPath))
            throw new Exception($" TaskRules Not Exist in :{tollTaxRulesPath}");

        var tollTaxRulesJson = File.ReadAllText(tollTaxRulesPath);
        var tollTaxRules = JsonConvert.DeserializeObject<List<TollTaxRule>>(tollTaxRulesJson);
        return new TollTaxRuleSet(tollTaxRules);
    }

    public TollTaxRuleSet(List<TollTaxRule> tollTaxRules)
    {
        _tollTaxRules = tollTaxRules;
    }

    public int GetFee(DateTime passingDate)
    {
        var tollFee = _tollTaxRules.Single(x => x.IsMach(new TimeOnly(passingDate.Hour, passingDate.Minute, passingDate.Second)));
        return tollFee.Amount;
    }
}

public class TollTaxRuleSetFactory
{
    public TollTaxRuleSet Create(string tollTaxRulesPath)
    {
        if (File.Exists(tollTaxRulesPath))
            throw new Exception($" TaskRules Not Exist in :{tollTaxRulesPath}");

        var tollTaxRulesJson = File.ReadAllText(tollTaxRulesPath);
        var tollTaxRules = JsonConvert.DeserializeObject<List<TollTaxRule>>(tollTaxRulesJson);
        return new TollTaxRuleSet(tollTaxRules);
    }
}