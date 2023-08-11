using System.Text;
using Crawler.Domain.Enums.CrawleUrl;
using Crawler.Domain.Repository;

namespace Crawler.Domain.Contracts.CrawleUrl
{
    public class CrawleUrlQuery : SqlPage
    {
        public long Id { get; set; } = 0;

        public string Url { get; set; }

        public CrawleUrlStatus Status { get; set; }

        public string StartCrawledAt { get; set; }

        public string EndCrawledAt { get; set; }

        public string StartCreatedTime { get; set; }

        public string EndCreatedTime { get; set; }

        public int Retry { get; set; }

        public string Remark { get; set; }

        public override string ToString()
        {
            StringBuilder where = new StringBuilder();
            if (Id > 0)
            {
                where.Append(" AND Id = @Id");
            }

            if (!string.IsNullOrEmpty(Url))
            {
                where.Append(" AND Url = @Url");
            }

            if (Status > 0)
            {
                where.Append(" AND Status = @Status");
            }

            if (!string.IsNullOrEmpty(StartCrawledAt))
            {
                where.Append(" AND CrawledAt >= @StartCrawledAt");
            }

            if (!string.IsNullOrEmpty(EndCrawledAt))
            {
                where.Append(" AND CrawledAt <= @EndCrawledAt");
            }

            if (!string.IsNullOrEmpty(StartCreatedTime))
            {
                where.Append(" AND CreatedTime >= @StartCreatedTime");
            }

            if (!string.IsNullOrEmpty(EndCreatedTime))
            {
                where.Append(" AND CreatedTime <= @EndCreatedTime");
            }

            if (Retry > 0)
            {
                where.Append(" AND Retry = @Retry");
            }

            if (!string.IsNullOrEmpty(Remark))
            {
                where.Append(" AND Remark LIKE @Remark");
            }

            if (where.Length > 0)
            {
                where.Insert(0, " WHERE 1=1 ");
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                where.Append($" ORDER BY {OrderBy} {OrderByDesc}");
            }

            if (PageIndex > 0 && PageSize > 0)
            {
                where.Append($" LIMIT {PageIndex * PageSize},{PageSize}");
            }

            return where.ToString();
        }
    }
}