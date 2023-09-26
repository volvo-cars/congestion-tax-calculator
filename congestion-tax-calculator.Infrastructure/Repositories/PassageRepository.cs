using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Tax;
using congestion_tax_calculator.Infrastructure.Context;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class PassageRepository : GenericRepository<Passage>, IPassageRepository
    {
        private readonly AppDbContext _context;
        public PassageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
