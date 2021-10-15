using System.Threading.Tasks;

namespace Volvo.Domain.SharedKernel
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}