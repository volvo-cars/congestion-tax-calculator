using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Tax;
using congestion_tax_calculator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class TaxExemptionRuleRepository : GenericRepository<TaxExemptionRule>, ITaxExemptionRuleRepository
    {
        private readonly AppDbContext _context;
        public TaxExemptionRuleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TaxExemptionRule>> GetExempRulesByCityIdAsync(int cityId)
        {
            return await _context.TaxExemptionRules
                .Where(rule => rule.CityId == cityId)
                .ToListAsync();
        }
    }
}
