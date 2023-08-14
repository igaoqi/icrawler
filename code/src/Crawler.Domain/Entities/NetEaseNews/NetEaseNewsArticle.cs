using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.Domain.Entities.NetEaseNews
{
    [Table("NetEaseNewsArticle")]
    public class NetEaseNewsArticle : AggregateRoot<long>
    {
        public string Url { get; set; }

        public string Author { get; set; }

        public string PublisheTime { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Images { get; set; }
    }
}