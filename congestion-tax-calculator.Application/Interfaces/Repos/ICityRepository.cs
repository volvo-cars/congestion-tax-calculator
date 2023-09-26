using congestion_tax_calculator.Domain.Entities.Common;

namespace congestion_tax_calculator.Application.Interfaces.Repos
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<List<DateTime>> GetCityTollFreeSpecificDateByIdAsync(int cityId);
    }
}
