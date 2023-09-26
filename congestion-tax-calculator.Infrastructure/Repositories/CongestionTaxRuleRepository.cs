using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Tax;
using congestion_tax_calculator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class CongestionTaxRuleRepository : GenericRepository<CongestionTaxRule>, ICongestionTaxRuleRepository
    {
        private readonly AppDbContext _context;
        public CongestionTaxRuleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CongestionTaxRule>> GetRulesByCityIdAsync(int cityId)
        {
            return await _context.CongestionTaxRules
                .Where(rule => rule.CityId == cityId)
                .ToListAsync();
        }
    }
}
