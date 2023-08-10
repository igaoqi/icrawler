namespace Crawler.Domain.Entities.CrawleUrl
{
    /// <summary>
    /// 1 : not crawled, 2: crawled but failed , 3: crawled success
    /// </summary>
    public enum CrawleUrlStatus
    {
        None = 0,
        UnCrawled = 1,
        Failed = 2,
        Success = 3
    }
}