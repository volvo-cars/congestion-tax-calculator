using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace congestion_tax_calculator.Application.AppConfigs
{
    public static class AppServiceRegisteration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
