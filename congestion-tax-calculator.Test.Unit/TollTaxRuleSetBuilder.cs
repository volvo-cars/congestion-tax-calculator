using congestion.Service;

namespace congestion_tax_calculator.Test.Unit
{
    public class TollTaxRuleSetBuilder
    {
        private List<TollTaxRule> _tollTaxRules = new();

        public TollTaxRuleSetBuilder WithRule(TollTaxRule rule)
        {
            _tollTaxRules.Add(rule);
            return this;
        }

        public TollTaxRuleSetBuilder WithRules(ICollection<TollTaxRule> rules)
        {
            _tollTaxRules.AddRange(rules);
            return this;
        }

        public TollTaxRuleSet Build()
        {
            return new TollTaxRuleSet(_tollTaxRules);
        }
    }
}