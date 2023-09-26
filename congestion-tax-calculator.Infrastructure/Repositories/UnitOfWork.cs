using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Infrastructure.Context;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        private ICityRepository _cityRepository;
        private ITaxExemptionRuleRepository _taxExemptionRuleRepository;
        private ICongestionTaxRuleRepository _congestionTaxRuleRepository ;
        private IPassageRepository _passageRepository ;
        private ICountryRepository _countryRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public ICityRepository CityRepository => _cityRepository ??= new CityRepository(_context);
        public ICongestionTaxRuleRepository CongestionTaxRuleRepository => _congestionTaxRuleRepository ??= new CongestionTaxRuleRepository(_context);
        public ITaxExemptionRuleRepository TaxExemptionRuleRepository => _taxExemptionRuleRepository ??= new TaxExemptionRuleRepository(_context);
        public IPassageRepository PassageRepository => _passageRepository ??= new PassageRepository(_context);
        public ICountryRepository CountryRepository => _countryRepository ??= new CountryRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
