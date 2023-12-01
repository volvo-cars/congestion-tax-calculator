using congestion.Interface;
using congestion.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.Infra
{
    public class TaxRuleEngine
    {
        private readonly List<ITaxRule> _rules = new List<ITaxRule>();

        public void AddRule(ITaxRule rule)
        {
            _rules.Add(rule);
        }

        public decimal CalculateTax(TaxPayer taxPayer)
        {
            decimal taxAmount =0;
            foreach (var rule in _rules)
            {
                taxAmount = rule.CalculateTax(taxPayer);
                if (taxAmount == 0)
                {
                    taxPayer.TaxedAmount = 0;
                    break;
                }
                taxPayer.TaxedAmount += taxAmount;
            }
            return taxPayer.TaxedAmount;
        }
    }
}
