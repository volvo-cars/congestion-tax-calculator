using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volvo.CongestionTax.Infrastructure.EFCore;

namespace Volvo.CongestionTax.WebAPI.IntegrationTests
{
    public class TestHost<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // ReSharper disable once AsyncVoidLambda
            builder.ConfigureServices(async services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<CongestionTaxDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTests");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CongestionTaxDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<TestHost<TStartup>>>();

                    await context.Database.EnsureCreatedAsync();

                    try
                    {
                        await DbExampleSeeder.SeedSampleDataAsync(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}", ex.Message);
                    }
                }
            });
        }
    }
}