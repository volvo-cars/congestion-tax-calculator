using System;

namespace Volvo.CongestionTax.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public abstract class AuditableEntity<TKey> : AuditableEntity
    {
        public TKey Id { get; set; }
    }
}
