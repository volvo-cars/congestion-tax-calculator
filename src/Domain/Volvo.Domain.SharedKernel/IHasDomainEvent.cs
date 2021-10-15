using System.Collections.Generic;

namespace Volvo.Domain.SharedKernel
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}