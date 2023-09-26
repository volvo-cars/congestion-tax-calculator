using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> GetCityTollFreeSpecificDateByIdAsync(int cityId)
        {
            var xx= await _context.Cities
                .Where(rule => rule.Id == cityId)
                .Select(x => x.TollFreeSpecificDates).Cast<DateTime>()
                .ToListAsync();
            return xx;
        }
    }
}
