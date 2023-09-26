using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Domain.Entities.Common;
using congestion_tax_calculator.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion_tax_calculator.Infrastructure.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {

        }
    }
}