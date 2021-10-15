using Microsoft.Extensions.DependencyInjection;
using Volvo.CongestionTax.Infrastructure.EFCore.Repository;
using Volvo.Infrastructure.SharedKernel.Repositories;

namespace Volvo.CongestionTax.Infrastructure.EFCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEfCoreInfastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddTransient<CongestionTaxDbContext>();

            return services;
        }
    }
}