using Crawler.Domain.Enums.CrawleUrl;

namespace Crawler.Domain.Entities.CrawleUrl
{
    public class CrawleUrl
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public DateTime CrawledAt { get; set; }

        /// <summary>
        /// 1 : not crawled, 2: crawled but failed , 3: crawled success
        /// </summary>
        public CrawleUrlStatus Status { get; set; }

        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Remark for failed or other
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Failed retry times,max is 3q
        /// </summary>
        public int Retry { get; set; }
    }
}