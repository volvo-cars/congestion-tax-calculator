namespace congestion_tax_calculator.Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        ITaxExemptionRuleRepository TaxExemptionRuleRepository { get; }
        ICongestionTaxRuleRepository CongestionTaxRuleRepository { get; }
        IPassageRepository PassageRepository { get; }
        ICountryRepository CountryRepository { get; }

        Task Save();
    }
}
