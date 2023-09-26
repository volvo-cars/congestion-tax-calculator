using congestion_tax_calculator.Domain.Entities.Tax;

namespace congestion_tax_calculator.Application.Interfaces.Repos
{
    public interface ICongestionTaxRuleRepository : IGenericRepository<CongestionTaxRule>
    {
        Task<List<CongestionTaxRule>> GetRulesByCityIdAsync(int cityId);
    }
}
