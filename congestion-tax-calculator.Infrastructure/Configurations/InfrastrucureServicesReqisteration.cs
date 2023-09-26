using congestion_tax_calculator.Application.Interfaces.Repos;
using congestion_tax_calculator.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion_tax_calculator.Infrastructure.Configurations
{
    public static class InfrastrucureServicesReqisteration
    {
        public static IServiceCollection ConfigurInfrastractureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ICityRepository, CityRepository>();
            //services.AddScoped<ICongestionTaxRuleRepository, CongestionTaxRuleRepository>();
            //services.AddScoped<ICountryRepository, CountryRepository>();
            //services.AddScoped<IHolidayService, HolidayService>();
            //services.AddScoped<IPassageRepository, PassageRepository>();
            //services.AddScoped<ITaxExemptionRuleRepository, TaxExemptionRuleRepository>();

            return services;
        }
    }
}
