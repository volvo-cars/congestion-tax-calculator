using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volvo.Application.SharedKernel.Behaviour;

namespace Volvo.Application.SharedKernel.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationSharedKernel(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<>));

            return services;
        }
    }
}