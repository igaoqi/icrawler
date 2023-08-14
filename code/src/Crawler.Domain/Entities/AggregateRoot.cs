using System.ComponentModel.DataAnnotations;

namespace Crawler.Domain.Entities
{
    public abstract class AggregateRoot<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }

        public virtual TKey? TenantId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual TKey? CreatedBy { get; set; }

        public virtual DateTime? UpdatedTime { get; set; }

        public virtual TKey? UpdatedBy { get; set; }
    }
}