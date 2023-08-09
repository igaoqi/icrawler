namespace Crawler.Domain.Entities
{
    public class CrawleUrl
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public DateTime CrawledAt { get; set; }

        /// <summary>
        /// 0 : not crawled, 1: crawled, 2: crawled but failed
        /// </summary>
        public int Status { get; set; }

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