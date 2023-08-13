using System;

namespace Crawler.Domain.Entities
{
    public class AggregateRoot<TKey>
    {
        public TKey Id { get; set; }

        public TKey TenantId { get; set; } = default;

        public DateTime CreatedTime { get; set; }

        public TKey CreatedBy { get; set; } = default;

        public DateTime UpdatedTime { get; set; }

        public TKey UpdatedBy { get; set; } = default;
    }
}