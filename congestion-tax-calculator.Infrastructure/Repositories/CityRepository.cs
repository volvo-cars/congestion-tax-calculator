using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Infrastructure.Context;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {

        }

        Task<List<DateTime>> ICityRepository.GetCityTollFreeSpecificDateByIdAsync(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
