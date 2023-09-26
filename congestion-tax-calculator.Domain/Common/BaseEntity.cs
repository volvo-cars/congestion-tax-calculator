namespace congestion_tax_calculator.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime RemoveTime { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<Guid>
    {

    }
}
