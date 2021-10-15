using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volvo.CongestionTax.Domain.Entities;
using Volvo.Domain.SharedKernel;
using AuditableEntity = Volvo.CongestionTax.Domain.Entities.AuditableEntity;

namespace Volvo.CongestionTax.Infrastructure.EFCore
{
    public class CongestionTaxDbContext : DbContext
    {
        private readonly IDomainEventService _domainEventService;

        protected CongestionTaxDbContext(DbContextOptions<CongestionTaxDbContext> options) : base(options)
        {
        }

        public CongestionTaxDbContext(DbContextOptions<CongestionTaxDbContext> options,
            IDomainEventService domainEventService) : this(options)
        {
            _domainEventService = domainEventService;
        }

        public DbSet<CityCongestionTaxRules> CityCongestionTaxRules { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateLastModified = DateTime.UtcNow;
                        break;
                }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}